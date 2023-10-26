using UnityEngine;

namespace Unite
{
    public abstract class EnemyDetectionData : ScriptableObject
    {
        public abstract bool IsTargetDetected(EnemyStateMachine enemy);
    }
}