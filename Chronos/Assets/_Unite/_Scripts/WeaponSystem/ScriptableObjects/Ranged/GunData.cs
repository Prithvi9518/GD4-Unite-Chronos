using System;
using System.Collections;
using Unite.Core.DamageInterfaces;
using Unite.SoundScripts;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Unite.WeaponSystem
{
    [CreateAssetMenu(fileName = "GunData", menuName = "Weapons/Gun")]
    public class GunData : ScriptableObject, ICloneable
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

        private MonoBehaviour activeMonoBehaviour;
        private GameObject model;
        private float lastShootTime;
        private ParticleSystem shootParticleSystem;
        private ObjectPool<TrailRenderer> trailPool;

        public GunType GunType => gunType;
        public ShootData ShootData => shootData;

        public void Spawn(Transform parent, MonoBehaviour monoBehaviour)
        {
            activeMonoBehaviour = monoBehaviour;
            lastShootTime = 0;
            trailPool = new ObjectPool<TrailRenderer>(CreateTrail);

            model = Instantiate(modelPrefab, parent, false);
            model.transform.localPosition = spawnPoint;
            model.transform.localRotation = Quaternion.Euler(spawnRotation);

            shootParticleSystem = model.GetComponentInChildren<ParticleSystem>();
        }

        public void Shoot()
        {
            if (!(Time.time > shootData.FireRate + lastShootTime)) return;
            
            lastShootTime = Time.time;
                
            shootParticleSystem.Play();
            audioConfig.PlayShootingAudioClip();
                
            var shootDirection = GetShootDirection();

            Vector3 start = shootParticleSystem.transform.position;

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
        }

        private Vector3 GetShootDirection()
        {
            Vector3 shootDirection = shootParticleSystem.transform.forward
                                     + new Vector3(
                                         Random.Range(-shootData.BulletSpread.x, shootData.BulletSpread.x),
                                         Random.Range(-shootData.BulletSpread.y, shootData.BulletSpread.y),
                                         Random.Range(-shootData.BulletSpread.z, shootData.BulletSpread.z)
                                     );
            shootDirection.Normalize();
            return shootDirection;
        }

        private void TryDealDamage(RaycastHit hit, float distance)
        {
            if (hit.collider == null) return;

            if (hit.collider.TryGetComponent(out ITakeDamage damageable))
            {
                damageable.TakeDamage(damageConfig.GetDamage(distance));
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

            return trail;
        }

        private IEnumerator FireBulletWithTrail(Vector3 start, Vector3 end, RaycastHit hit)
        {
            TrailRenderer instance = trailPool.Get();
            instance.gameObject.SetActive(true);
            instance.transform.position = start;

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
            
            TryDealDamage(hit, distance);

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
    }
}