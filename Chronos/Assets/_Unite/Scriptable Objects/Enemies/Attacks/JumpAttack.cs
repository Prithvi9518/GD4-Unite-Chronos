using System.Collections;
using UnityEngine;

namespace Unite
{
    [CreateAssetMenu(fileName = "JumpAttack", menuName = "Unite/Scriptable Objects/Enemies/Attacks/Jump Attack")]
    public class JumpAttack : AttackData
    {
        [SerializeField]
        private AnimationCurve heightCurve;

        [SerializeField]
        private float jumpSpeed;

        public override bool CanUseAttack(EnemyStateMachine enemy)
        {
            return timeWhenLastAttacked + attackCooldown < Time.time;
        }

        public override void DoAttack(EnemyStateMachine enemy)
        {
            enemy.StartCoroutine(JumpAttackCoroutine(enemy));
        }

        private IEnumerator JumpAttackCoroutine(EnemyStateMachine enemy)
        {
            enemy.Agent.enabled = false;

            Vector3 startPos = enemy.transform.position;
            Vector3 targetPos = enemy.Target.transform.position;

            for (float time = 0; time < 1; time += Time.deltaTime * jumpSpeed)
            {
                enemy.transform.position = Vector3.Lerp(startPos, targetPos, time)
                    + Vector3.up * heightCurve.Evaluate(time);

                yield return null;
            }

            timeWhenLastAttacked = Time.time;

            enemy.Agent.enabled = true;
        }
    }
}