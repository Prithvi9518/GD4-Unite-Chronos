using System.Collections;
using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies
{
    [CreateAssetMenu(fileName = "FireBreathAttack", menuName = "Enemies/Attacks/FireBreathAttack")]
    public class FireBreathAttack : AttackData
    {
        [SerializeField]
        private float durationInSeconds;
        
        [SerializeField]
        private float damageTickRate;

        [SerializeField]
        private Vector3 localPosition;

        [SerializeField] 
        private GameObject prefab;

        public float DurationInSeconds => durationInSeconds;
        public float DamageTickRate => damageTickRate;
        public GameObject Prefab => prefab;

        public override void Attack(Enemy enemy)
        {
            // enemy.StartCoroutine(BreatheFire(enemy));
            FireBreathHandler handler = enemy.StateMachine.GetComponent<FireBreathHandler>();
            handler.BreatheFire(enemy, this);
        }

        public override bool CheckDealDamage(Enemy enemy)
        {
            return true;
        }

        private IEnumerator BreatheFire(Enemy enemy)
        {
            WaitForSeconds wait = new WaitForSeconds(durationInSeconds);
            
            GameObject fireBreathPrefab = Instantiate(prefab, enemy.transform, false);
            if (fireBreathPrefab != null)
            {
                fireBreathPrefab.transform.localPosition = localPosition;
                AreaDamage areaDamage = fireBreathPrefab.GetComponentInChildren<AreaDamage>();
                float totalDamage = enemy.AttackHandler.GetTotalDamage(this);
                areaDamage.PerformSetup(totalDamage, damageTickRate, enemy.AttackHandler, this);

                // for (float time = 0; time < durationInSeconds; time += Time.deltaTime)
                // {
                //     enemy.transform.LookAt(enemy.DetectionHandler.Target);
                //     yield return null;
                // }

                yield return wait;
                
                Destroy(fireBreathPrefab);
                enemy.StateMachine.TriggerStateEvent(StateEvent.AttackDurationFinished);
            }
        }
    }
}