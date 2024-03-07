using Unite.Core.DamageInterfaces;
using Unite.Managers;
using Unite.StatusEffectSystem;
using Unite.TimeStop;
using UnityEngine;
using UnityEngine.Pool;

namespace Unite.Projectiles
{
    public class Projectile : MonoBehaviour, ITimeStopSubscriber
    {
        [SerializeField]
        private string id;
        
        [SerializeField]
        protected float moveSpeed;
        
        [SerializeField]
        protected StatusEffectSO statusEffect;

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

        public string ID => id;
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

        private void OnEnable()
        {
            GameManager.Instance.OnGameLose += DestroySelf;
            GameManager.Instance.OnGameRestart += DestroySelf;
        }
        
        private void OnDisable()
        {
            GameManager.Instance.OnGameLose -= DestroySelf;
            GameManager.Instance.OnGameRestart -= DestroySelf;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out ITakeDamage damageable)) return;
            damageable.TakeDamage(damage, shooter, damager);
            
            if (other.TryGetComponent(out IStatusEffectable effectable))
            {
                if(statusEffect != null)
                    effectable.ApplyStatusEffect(statusEffect, shooter);
            }
                
            Disable();
        }

        public void PerformSetup(ProjectileContext context)
        {
            damage = context.DamageAmount;
            projectilePool = context.Pool;
            shooter = context.AttackingEntity;
            damager = context.ShotWith;
            projectileTarget = context.Target;
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

        private void DestroySelf()
        {
            Destroy(gameObject);
            // Disable();
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