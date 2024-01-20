using Unite.StatePattern;
using Unite.TimeStop;
using UnityEngine;

namespace Unite.Enemies
{
    public class FireBreathHandler : MonoBehaviour, ITimeStopSubscriber
    {
        [SerializeField]
        private Transform spawnPoint;
        
        private Enemy enemy;
        private FireBreathAttack attack;

        private GameObject fireBreath;
        private bool fireBreathActive;

        private float timer;
        
        private void Update()
        {
            if (!fireBreathActive) return;
            if (timer >= attack.DurationInSeconds)
            {
                OnCompleteFireBreath();
                return;
            }

            timer += Time.deltaTime;
        }

        public void BreatheFire(Enemy enemy, FireBreathAttack attack)
        {
            this.enemy = enemy;
            this.attack = attack;

            fireBreath = Instantiate(attack.Prefab, spawnPoint, false);
            if (fireBreath == null) return;
            AreaDamage areaDamage = fireBreath.GetComponentInChildren<AreaDamage>();
            float totalDamage = enemy.AttackHandler.GetTotalDamage(attack);
            areaDamage.PerformSetup(totalDamage, attack.DamageTickRate, enemy.AttackHandler, attack);

            fireBreathActive = true;
        }

        private void OnCompleteFireBreath()
        {
            if (fireBreath == null) return;
            
            Destroy(fireBreath);
            enemy.StateMachine.TriggerStateEvent(StateEvent.AttackDurationFinished);
            fireBreathActive = false;
            timer = 0;
        }

        public void HandleTimeStopEvent(bool isTimeStopped)
        {
            fireBreathActive = !isTimeStopped;
        }
    }
}