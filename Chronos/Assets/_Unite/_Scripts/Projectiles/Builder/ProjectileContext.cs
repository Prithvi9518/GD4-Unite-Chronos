using Unite.Core.DamageInterfaces;
using UnityEngine;
using UnityEngine.Pool;

namespace Unite.Projectiles
{
    public class ProjectileContext
    {
        public float DamageAmount { get; set; }
        public ObjectPool<Projectile> Pool { get; set; }
        public IAttacker AttackingEntity { get; set; }
        public IDoDamage ShotWith { get; set; }
        public Transform Target { get; set; }
    }
}