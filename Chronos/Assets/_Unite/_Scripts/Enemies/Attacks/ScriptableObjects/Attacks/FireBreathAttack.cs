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
        private GameObject prefab;

        public float DurationInSeconds => durationInSeconds;
        public float DamageTickRate => damageTickRate;
        public GameObject Prefab => prefab;

        public override void Attack(Enemy enemy)
        {
            FireBreathHandler handler = enemy.StateMachine.GetComponent<FireBreathHandler>();
            handler.BreatheFire(enemy, this);
        }

        public override bool CheckDealDamage(Enemy enemy)
        {
            return true;
        }
    }
}