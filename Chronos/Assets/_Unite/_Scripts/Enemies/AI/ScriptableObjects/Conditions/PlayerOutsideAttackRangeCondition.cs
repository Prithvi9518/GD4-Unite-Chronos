using System.Collections.Generic;
using Unite.StatePattern;
using UnityEngine;

namespace Unite.Enemies.AI
{
    [CreateAssetMenu(fileName = "PlayerOutsideAttackRangeCondition", menuName = "AI/Conditions/PlayerOutsideAttackRangeCondition")]
    public class PlayerOutsideAttackRangeCondition : Condition
    {
        [SerializeField] 
        private AttackData attackToCheck;
        
        [SerializeField]
        private bool checkLowerRange;
        
        [SerializeField]
        private bool checkUpperRange;
        
        public override bool VerifyCondition(BaseStateMachine baseStateMachine)
        {
            Enemy enemy = baseStateMachine.GetComponent<Enemy>();

            Attack attack = enemy.AttackHandler.Attacks.GetValueOrDefault(attackToCheck.name, null);

            if (checkLowerRange)
            {
                return attack.AttackData.OutsideLowerRange(enemy.transform, enemy.DetectionHandler.Target);
            }

            if (checkUpperRange)
            {
                return attack.AttackData.OutsideUpperRange(enemy.transform, enemy.DetectionHandler.Target);
            }

            return false;
        }
    }
}