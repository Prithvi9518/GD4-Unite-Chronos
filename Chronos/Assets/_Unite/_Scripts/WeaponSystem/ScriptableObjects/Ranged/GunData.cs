using System;
using System.Collections;
using Unite.Core.DamageInterfaces;
using Unite.ImpactSystem;
using Unite.SoundScripts;
using Unite.StatusEffectSystem;
using Unite.VFXScripts;
using Unite.WeaponSystem.ImpactEffects;
using UnityEngine;
using UnityEngine.Pool;

namespace Unite.WeaponSystem
{
    [CreateAssetMenu(fileName = "GunData", menuName = "Weapons/Gun")]
    public class GunData : ScriptableObject, ICloneable, IDoDamage
    {
        [SerializeField] 
        private GunType gunType;
        
        [SerializeField] 
        private string gunName;

        [SerializeField] 
        private GameObject modelPrefab;

        [SerializeField]
        private Vector3 spawnPoint;
        
        [SerializeField]
        private Vector3 spawnRotation;

        [SerializeField] 
        private ShootData shootData;

        [SerializeField]
        private DamageConfig damageConfig;

        [SerializeField] 
        private BulletTrailData bulletTrailData;

        [SerializeField] 
        private GunAudioConfig audioConfig;

        [SerializeField] 
        private ImpactType impactType;
        
        private IAttacker shooter;
        
        private MonoBehaviour activeMonoBehaviour;
        private GameObject model;
        private float lastShootTime;
        private float initialClickTime;
        private float stopShootingTime;
        private bool wantedToShootLastFrame;
        private MuzzleFlashHandler muzzleFlashHandler;
        private GameObject trailsGameObject;
        private ObjectPool<TrailRenderer> trailPool;
        private IImpactHandler[] bulletImpactEffects = Array.Empty<IImpactHandler>();

        private float baseDamage;

        private Transform shootPoint;

        public IAttacker Shooter => shooter;
        public GunType GunType => gunType;
        public ShootData ShootData => shootData;
        public DamageConfig DamageConfig => damageConfig;
        public float BaseDamage => baseDamage;

        public void SetImpactType(ImpactType type)
        {
            impactType = type;
        }

        public void UpdateBaseDamage(float updatedValue)
        {
            baseDamage = updatedValue;
        } 

        public void SetBulletImpactEffects(IImpactHandler[] effects)
        {
            bulletImpactEffects = effects;
        }

        public void Spawn(Transform parent, MonoBehaviour monoBehaviour)
        {
            activeMonoBehaviour = monoBehaviour;
            lastShootTime = 0;

            shooter = activeMonoBehaviour.GetComponent<IAttacker>();

            trailsGameObject = new GameObject("Bullet Trails")
            {
                transform =
                {
                    position = Vector3.zero
                }
            };
            trailsGameObject.transform.SetParent(null);

            trailPool = new ObjectPool<TrailRenderer>(
                CreateTrail,
                trail =>
                {
                    trail.transform.parent = null;
                },
                trail =>
                {
                    trail.enabled = false;
                    trail.transform.parent = trailsGameObject.transform;
                },
                DestroyTrailData
            );

            model = Instantiate(modelPrefab, parent, false);
            model.transform.localPosition = spawnPoint;
            model.transform.localRotation = Quaternion.Euler(spawnRotation);

            muzzleFlashHandler = model.GetComponent<MuzzleFlashHandler>();
            shootPoint = model.GetComponent<ShootPointHolder>().ShootPoint;
        }

        public void Tick(bool wantsToShoot)
        {
            model.transform.localRotation = Quaternion.Slerp(
                model.transform.localRotation,
                Quaternion.Euler(spawnRotation),
                Time.deltaTime * shootData.RecoilRecoverySpeed
            );
            
            if (wantsToShoot)
            {
                wantedToShootLastFrame = true;
                Shoot();
            }
            else if (!wantsToShoot && wantedToShootLastFrame)
            {
                stopShootingTime = Time.time;
                wantedToShootLastFrame = false;
            }
        }

        public void Shoot()
        {
            if (Time.time - lastShootTime - shootData.FireRate > Time.deltaTime)
            {
                float lastDuration = Mathf.Clamp(0, stopShootingTime - initialClickTime, shootData.MaxSpreadTime);
                float lerpTime = (shootData.RecoilRecoverySpeed - (Time.time - stopShootingTime)) / shootData.RecoilRecoverySpeed;
                
                initialClickTime = Time.time - Mathf.Lerp(0, lastDuration, Mathf.Clamp01(lerpTime));
            }
            
            if (!(Time.time > shootData.FireRate + lastShootTime)) return;
            
            lastShootTime = Time.time;
                
            audioConfig.PlayShootingAudioClip();

            Vector3 bulletSpread = shootData.GetSpread(Time.time - initialClickTime);
            model.transform.forward += model.transform.TransformDirection(bulletSpread);
            Vector3 shootDirection = model.transform.forward;

            // Vector3 start = muzzleFlashHandler.transform.position;
            Vector3 start = shootPoint.position;

            if (Physics.Raycast(start, shootDirection, out RaycastHit hit,
                    float.MaxValue, shootData.HitMask))
            {
                activeMonoBehaviour.StartCoroutine(
                    FireBulletWithTrail(
                        start,
                        hit.point,
                        hit
                    )
                );
            }
            else
            {
                activeMonoBehaviour.StartCoroutine(
                    FireBulletWithTrail(
                        start,
                        start + (shootDirection * bulletTrailData.MissDistance),
                        new RaycastHit()
                    )
                );
            }
            
            muzzleFlashHandler.PlayMuzzleFlash();
        }

        private void HandleBulletImpact(RaycastHit hit, float distance)
        {
            SurfaceManager.Instance.HandleImpact(
                hit.collider.gameObject, 
                hit.point,
                hit.normal, 
                impactType
            );
            
            if (hit.collider.TryGetComponent(out ITakeDamage damageable))
            {
                damageable.TakeDamage(baseDamage + damageConfig.GetDamage(distance),
                    shooter, this);
            }
            if (hit.collider.TryGetComponent(out IStatusEffectable effectable))
            {
                if(damageConfig.StatusEffect != null)
                    effectable.ApplyStatusEffect(damageConfig.StatusEffect, shooter);
            }

            foreach (IImpactHandler impactHandler in bulletImpactEffects)
            {
                impactHandler.HandleImpact(hit.collider, hit.point, hit.normal, this);
            }
        }

        private TrailRenderer CreateTrail()
        {
            GameObject instance = new GameObject("Bullet Trail");

            TrailRenderer trail = instance.AddComponent<TrailRenderer>();
            trail.colorGradient = bulletTrailData.Color;
            trail.material = bulletTrailData.Material;
            trail.widthCurve = bulletTrailData.WidthCurve;
            trail.time = bulletTrailData.Duration;
            trail.minVertexDistance = bulletTrailData.MinVertexDistance;

            trail.emitting = false;
            trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            trail.transform.SetParent(trailsGameObject.transform);

            return trail;
        }

        private void DestroyTrailData(TrailRenderer trail)
        {
            Destroy(trail.gameObject);
        }

        private IEnumerator FireBulletWithTrail(Vector3 start, Vector3 end, RaycastHit hit)
        {
            TrailRenderer instance = trailPool.Get();
            instance.transform.position = start;
            instance.transform.rotation = Quaternion.identity;
            instance.Clear();
            instance.gameObject.SetActive(true);
            instance.enabled = true;

            yield return null; // avoid position carry-over from last frame if reused

            instance.emitting = true;

            float distance = Vector3.Distance(start, end);
            float remainingDistance = distance;
            while (remainingDistance > 0)
            {
                instance.transform.position = Vector3.Lerp(
                    start,
                    end,
                    Mathf.Clamp01(1 - (remainingDistance/distance))
                );

                remainingDistance -= bulletTrailData.SimulationSpeed * Time.deltaTime;

                yield return null;
            }

            instance.transform.position = end;

            if (hit.collider != null)
            {
                HandleBulletImpact(hit, distance);
            }

            yield return new WaitForSeconds(bulletTrailData.Duration);
            yield return null;
            instance.emitting = false;
            instance.gameObject.SetActive(false);
            trailPool.Release(instance);
        }

        public object Clone()
        {
            GunData clone = CreateInstance<GunData>();
            clone.gunType = gunType;
            clone.gunName = gunName;
            clone.name = name;
            clone.damageConfig = damageConfig.Clone() as DamageConfig;
            clone.shootData = shootData.Clone() as ShootData;
            clone.audioConfig = audioConfig.Clone() as GunAudioConfig;
            clone.bulletTrailData = bulletTrailData.Clone() as BulletTrailData;

            clone.modelPrefab = modelPrefab;
            clone.spawnPoint = spawnPoint;
            clone.spawnRotation = spawnRotation;
            
            return clone;
        }

        public string GetName()
        {
            return name;
        }

        public DamageType GetDamageType()
        {
            return DamageType.Direct;
        }
    }
}