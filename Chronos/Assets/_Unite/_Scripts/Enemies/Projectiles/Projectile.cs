using Unite.Core.DamageInterfaces;
using UnityEngine;
using UnityEngine.Pool;

namespace Unite.Enemies.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        [SerializeField]
        private float autoDestroyTimeInSeconds;

        private Rigidbody rb;

        private float damage;

        private ObjectPool<Projectile> projectilePool;

        public float Speed => speed;
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

            damageable.TakeDamage(damage);

            Disable();
        }

        public void PerformSetup(float damageAmount, ObjectPool<Projectile> pool)
        {
            damage = damageAmount;
            projectilePool = pool;
        }

        private void Disable()
        {
            CancelInvoke(nameof(Disable));
            rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
            projectilePool.Release(this);
        }
    }
}