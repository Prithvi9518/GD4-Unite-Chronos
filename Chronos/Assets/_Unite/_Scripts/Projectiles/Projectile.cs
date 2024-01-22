using Unite.Core.DamageInterfaces;
using Unite.TimeStop;
using UnityEngine;
using UnityEngine.Pool;

namespace Unite.Projectiles
{
    public class Projectile : MonoBehaviour, ITimeStopSubscriber
    {
        [SerializeField]
        protected float moveSpeed;

        [SerializeField]
        private float autoDestroyTimeInSeconds;

        [SerializeField]
        protected float yOffset = 1f;

        protected Rigidbody rb;

        private IAttacker shooter;
        private IDoDamage damager;
        private float damage;

        protected Transform projectileTarget;

        private ObjectPool<Projectile> projectilePool;

        private Vector3 velocityBeforeTimeStop;

        public float MoveSpeed => moveSpeed;
        public Rigidbody Rigidbody => rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            CancelInvoke(nameof(Disable));
            Invoke(nameof(Disable), autoDestroyTimeInSeconds);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out ITakeDamage damageable)) return;

            damageable.TakeDamage(damage, shooter, damager);

            Disable();
        }

        public void PerformSetup(float damageAmount, ObjectPool<Projectile> pool,
            IAttacker attackingEntity, IDoDamage shotWith, Transform target)
        {
            damage = damageAmount;
            projectilePool = pool;
            shooter = attackingEntity;
            damager = shotWith;
            projectileTarget = target;
        }

        public virtual void Spawn()
        {
            Vector3 targetPosWithOffset = projectileTarget.position + (Vector3.up * yOffset);
            Vector3 shootDir = (targetPosWithOffset - transform.position).normalized;
            rb.AddForce(shootDir * moveSpeed, ForceMode.VelocityChange);
        }
        
        private void Disable()
        {
            CancelInvoke(nameof(Disable));
            rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
            projectilePool.Release(this);
        }

        public virtual void HandleTimeStopEvent(bool isTimeStopped)
        {
            if (isTimeStopped)
            {
                velocityBeforeTimeStop = rb.velocity;
                rb.velocity = Vector3.zero;
                CancelInvoke(nameof(Disable));
            }
            else
            {
                rb.velocity = velocityBeforeTimeStop;
                Invoke(nameof(Disable), autoDestroyTimeInSeconds);
            }
        }
    }
}