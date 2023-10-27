using UnityEngine;

namespace Unite
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimationHandler : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
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
    }
}