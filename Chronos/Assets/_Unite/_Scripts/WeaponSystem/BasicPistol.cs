using System.Collections;
using Unite.Core.DamageInterfaces;
using Unite.Core.Player;
using UnityEngine;
using UnityEngine.Pool;

namespace Unite.WeaponSystem
{
    public class BasicPistol : MonoBehaviour, IDoDamage
    {
        [SerializeField] private Camera cam;

        [SerializeField] private float range = 100f;
        [SerializeField] private float damage = 25f;

        [SerializeField]
        private float timeBetweenShots = 0.5f;

        [SerializeField]
        private ParticleSystem muzzleFlashEffect;

        [SerializeField]
        private BulletTrailData bulletTrailData;

        private bool canShoot = true;

        private ObjectPool<TrailRenderer> trailPool;

        public delegate void HandleBasicPistolShoot();
        public static event HandleBasicPistolShoot OnBasicPistolShoot;

        private void Start()
        {
            trailPool = new ObjectPool<TrailRenderer>(CreateTrail);
        }
        
        private void OnEnable()
        {
            canShoot = true; // prevents shooting from being locked after switching to a new weapon
        }
        
        public void RespondToShootActionEvent()
        {
            if (!canShoot) return;
            StartCoroutine(Shoot());
        }
        
        private IEnumerator Shoot()
        {
            canShoot = false;
            
            PlayMuzzleFlash();
            FireRaycast();
            InvokePistolShootEvent();
            yield return new WaitForSeconds(timeBetweenShots);

            canShoot = true;
        }
        
        private void InvokePistolShootEvent()
        {
            OnBasicPistolShoot?.Invoke();
        }
        
        private void FireRaycast()
        {
            RaycastHit hit;
            bool raycast = Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range);

            if (!raycast)
            {
                StartCoroutine(PlayBulletTrail(
                        muzzleFlashEffect.transform.position,
                        muzzleFlashEffect.transform.position + (cam.transform.forward * bulletTrailData.MissDistance),
                        new RaycastHit()
                    )
                );
                return;
            }

            StartCoroutine(PlayBulletTrail(
                    muzzleFlashEffect.transform.position,
                    hit.point,
                    hit
                )
            );

            DoDamage(hit.transform.gameObject);
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
        
        private void PlayMuzzleFlash()
        {
            if (muzzleFlashEffect == null) return;
            muzzleFlashEffect.Play();
        }

        private IEnumerator PlayBulletTrail(Vector3 startPos, Vector3 endPos, RaycastHit hit)
        {
            TrailRenderer instance = trailPool.Get();
            instance.gameObject.SetActive(true);
            instance.transform.position = startPos;
            yield return null; // wait for 1 frame to avoid position to be carried over from last frame if object is re-used from the pool

            instance.emitting = true;

            float distance = Vector3.Distance(startPos, endPos);
            float remainingDistance = distance;
            while (remainingDistance > 0)
            {
                instance.transform.position = Vector3.Lerp(startPos, endPos, Mathf.Clamp01(1 - (remainingDistance / distance)));
                remainingDistance -= bulletTrailData.SimulationSpeed * Time.deltaTime;

                yield return null;
            }

            instance.transform.position = endPos;

            yield return new WaitForSeconds(bulletTrailData.Duration);
            yield return null;
            instance.emitting = false;
            instance.gameObject.SetActive(false);
            trailPool.Release(instance);
        }

        public void DoDamage(GameObject target)
        {
            ITakeDamage damageable = target.transform.GetComponent<ITakeDamage>();
            if (damageable == null) return;

            damageable.TakeDamage(damage);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(cam.transform.position, cam.transform.forward * range);
        }
    }
}