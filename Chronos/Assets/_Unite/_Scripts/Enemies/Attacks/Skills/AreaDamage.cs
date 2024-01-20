using System.Collections;
using Unite.Core.DamageInterfaces;
using UnityEngine;

namespace Unite.Enemies
{
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
            Debug.Log($"OnTriggerEnter. targetDamageable = {targetDamageable}");
            StartCoroutine(DealDamageCoroutine());
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out ITakeDamage damageable)) return;
            targetDamageable = null;
            Debug.Log($"OnTriggerExit. targetDamageable = {targetDamageable}");
        }

        private IEnumerator DealDamageCoroutine()
        {
            WaitForSeconds wait = new WaitForSeconds(damageTickRate);

            while (targetDamageable != null)
            {
                targetDamageable.TakeDamage(damage, attacker, attack);
                Debug.Log("dealing damage");
                yield return wait;
            }
        }

        private void OnDisable()
        {
            targetDamageable = null;
        }
    }
}