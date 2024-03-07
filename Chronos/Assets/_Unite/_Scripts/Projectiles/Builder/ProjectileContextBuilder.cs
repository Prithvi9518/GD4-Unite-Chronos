using Unite.Core.DamageInterfaces;
using UnityEngine;
using UnityEngine.Pool;

namespace Unite.Projectiles
{
    public class ProjectileContextBuilder
    {
        private ProjectileContext context = new ProjectileContext();

        public ProjectileContextBuilder WithDamage(float damageAmount)
        {
            context.DamageAmount = damageAmount;
            return this;
        }
        
        public ProjectileContextBuilder WithPool(ObjectPool<Projectile> pool)
        {
            context.Pool = pool;
            return this;
        }
        
        public ProjectileContextBuilder WithAttacker(IAttacker attacker)
        {
            context.AttackingEntity = attacker;
            return this;
        }
        
        public ProjectileContextBuilder WithShooter(IDoDamage shotWith)
        {
            context.ShotWith = shotWith;
            return this;
        }
        
        public ProjectileContextBuilder WithTarget(Transform target)
        {
            context.Target = target;
            return this;
        }
        
        public ProjectileContext Build()
        {
            return context;
        }
    }
}