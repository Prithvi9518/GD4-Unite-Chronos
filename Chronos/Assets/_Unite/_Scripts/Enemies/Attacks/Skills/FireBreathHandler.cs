using Unite.StatePattern;
using Unite.TimeStop;
using UnityEngine;
using UnityEngine.Pool;

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
        
        private AreaDamage areaDamage;

        private float timer;

        private IObjectPool<GameObject> pool;

        private void Awake()
        {
            pool = new ObjectPool<GameObject>(CreateFireBreath,
                OnGetFireBreath,
                OnReleaseFireBreath);
        }

        private void Update()
        {
            if (!fireBreathActive) return;
            if (attack == null) return;
            if (timer >= attack.DurationInSeconds)
            {
                OnCompleteFireBreath();
                return;
            }

            timer += Time.deltaTime;
        }
        
        private GameObject CreateFireBreath()
        {
            return Instantiate(attack.Prefab, spawnPoint, false);
        }
        
        private void OnGetFireBreath(GameObject obj)
        {
            obj.SetActive(true);
            fireBreath = obj;
            areaDamage = obj.GetComponentInChildren<AreaDamage>();
        }
        
        private void OnReleaseFireBreath(GameObject obj)
        {
            obj.SetActive(false);
            fireBreath = null;
            areaDamage = null;
        }
        
        private void OnCompleteFireBreath()
        {
            if (fireBreath == null) return;
            
            pool.Release(fireBreath);
            enemy.StateMachine.TriggerStateEvent(StateEvent.AttackDurationFinished);
            fireBreathActive = false;
            timer = 0;
        }

        public void BreatheFire(Enemy enemy, FireBreathAttack attack)
        {
            this.enemy = enemy;
            this.attack = attack;

            fireBreath = pool.Get();
            float totalDamage = enemy.AttackHandler.GetTotalDamage(attack);
            areaDamage.PerformSetup(totalDamage, attack.DamageTickRate, enemy.AttackHandler, attack);
            fireBreathActive = true;
        }

        public void HandleTimeStopEvent(bool isTimeStopped)
        {
            fireBreathActive = !isTimeStopped;
        }
    }
}