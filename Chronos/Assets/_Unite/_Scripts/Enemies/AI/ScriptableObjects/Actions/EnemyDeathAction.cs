using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies.AI
{
    [CreateAssetMenu(fileName = "EnemyDeathAction", menuName = "AI/Actions/EnemyDeathAction")]
    public class EnemyDeathAction : FSMAction
    {
        public override void ExecuteAction(BaseStateMachine baseStateMachine)
        {
            Enemy enemy = baseStateMachine.GetComponent<Enemy>();
            EnemyDamager damager = baseStateMachine.GetComponent<EnemyDamager>();
            
            enemy.OnEnemyDeath();
        }
    }
}