using Unite.Core.DamageInterfaces;
using UnityEngine;

namespace Unite.Enemies.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private float speed;

        private Rigidbody rb;

        private IHandleAttacks attackHandler;
        private Attack attack;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            rb.AddForce(Vector3.forward * speed);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out ITakeDamage damageable)) return;

            float damage = attackHandler.GetTotalDamage(attack);
            damageable.TakeDamage(damage);
            
            Destroy(gameObject);
        }

        public void PerformSetup(IHandleAttacks handler, Attack attack)
        {
            attackHandler = handler;
            this.attack = attack;
        }
    }
}