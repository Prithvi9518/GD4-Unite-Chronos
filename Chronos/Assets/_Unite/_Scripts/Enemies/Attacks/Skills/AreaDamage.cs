using System.Collections;
using Unite.Core.DamageInterfaces;
using UnityEngine;

namespace Unite.Enemies
{
    /// <summary>
    /// Used by the fire-breath skill to deal damage within
    /// the area of the flame.
    ///
    /// <seealso cref="FireBreathHandler"/>
    /// <seealso cref="FireBreathAttack"/>
    /// </summary>
    public class AreaDamage : MonoBehaviour
    {
        private float damage;
        private float damageTickRate;
        private ITakeDamage targetDamageable;

        private IAttacker attacker;
        private IDoDamage attack;

        public void PerformSetup(float dmg, float tickRate, IAttacker attackUser, IDoDamage attackUsed)
        {
            damage = dmg;
            damageTickRate = tickRate;
            attacker = attackUser;
            attack = attackUsed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (targetDamageable != null) return;
            if (!other.TryGetComponent(out ITakeDamage damageable)) return;
            targetDamageable = damageable;
            StartCoroutine(DealDamageCoroutine());
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out ITakeDamage damageable)) return;
            targetDamageable = null;
        }

        private IEnumerator DealDamageCoroutine()
        {
            WaitForSeconds wait = new WaitForSeconds(damageTickRate);

            while (targetDamageable != null)
            {
                targetDamageable.TakeDamage(damage, attacker, attack);
                yield return wait;
            }
        }

        private void OnDisable()
        {
            targetDamageable = null;
        }
    }
}