using Unite.ImpactSystem;
using Unite.WeaponSystem.ImpactEffects;
using Unite.WeaponSystem.Modifiers;
using UnityEngine;

namespace Unite.BuffSystem
{
    [CreateAssetMenu(fileName = "ExplosiveBulletsBuff", menuName = "Buffs/Explosive Bullets")]
    public class ExplosiveBulletsBuff : BuffScriptableObject
    {
        [SerializeField] 
        private ImpactType impactType;
        
        [Header("Explosion Parameters")]
        [SerializeField] 
        private float radius;
        [SerializeField]
        private AnimationCurve damageFalloff;
        [SerializeField] 
        private int baseExplosionDamage;
        [SerializeField] 
        private int maxEnemiesAffected;

        /// <summary>
        /// Adds explosion effect modifier onto the player's gun.
        ///
        /// <seealso cref="ImpactTypeModifier"/>
        /// <seealso cref="ExplodeEffect"/>
        /// </summary>
        public override void ApplyBuff(Player.Player player)
        {
            base.ApplyBuff(player);
            
            ImpactTypeModifier modifier = new ImpactTypeModifier(impactType);
            ExplodeEffect explode = new ExplodeEffect(radius, damageFalloff,
                baseExplosionDamage, maxEnemiesAffected);
            player.GunHandler.ApplyModifier(modifier);
            player.GunHandler.ActiveGun.SetBulletImpactEffects(new IImpactHandler[]{explode});
        }
    }
}