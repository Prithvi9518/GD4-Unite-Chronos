using Unite.Core.DamageInterfaces;
using Unite.TimeStop;
using UnityEngine;
using UnityEngine.Pool;

namespace Unite.Enemies.Projectiles
{
    public class Projectile : MonoBehaviour, ITimeStopSubscriber
    {
        [SerializeField]
        private float moveSpeed;

        [SerializeField]
        private float autoDestroyTimeInSeconds;

        private Rigidbody rb;

        private IAttacker shooter;
        private IDoDamage damager;
        private float damage;

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
            IAttacker attackingEntity, IDoDamage shotWith)
        {
            damage = damageAmount;
            projectilePool = pool;
            shooter = attackingEntity;
            damager = shotWith;
        }

        private void Disable()
        {
            CancelInvoke(nameof(Disable));
            rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
            projectilePool.Release(this);
        }

        public void HandleTimeStopEvent(bool isTimeStopped)
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