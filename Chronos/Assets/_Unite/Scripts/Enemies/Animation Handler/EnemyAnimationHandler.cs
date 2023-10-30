using UnityEngine;

namespace Unite
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimationHandler : MonoBehaviour
    {
        private Animator animator;
        private EnemyAttackHandler attackHandler;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            attackHandler = GetComponent<EnemyAttackHandler>();
        }

        public void SetAnimationBool(string boolName, bool value)
        {
            animator.SetBool(boolName, value);
        }

        public void SetAnimationTrigger(string triggerName, bool isActive)
        {
            if (isActive)
                animator.SetTrigger(triggerName);
            else
                animator.ResetTrigger(triggerName);
        }

        public void ToggleAnimator(bool isEnabled)
        {
            animator.enabled = isEnabled;
        }

        /// <summary>
        /// This method is called when an attack animation event is triggered.
        /// We can pass in an object as an event parameter, so we pass in the AttackData object
        /// that we want to use.
        /// </summary>
        /// <param name="attackData"></param>
        public void HandleAttackAnimationEvent(AttackData attackData)
        {
            attackHandler.DealDamage(attackData.AttackType);
        }
    }
}