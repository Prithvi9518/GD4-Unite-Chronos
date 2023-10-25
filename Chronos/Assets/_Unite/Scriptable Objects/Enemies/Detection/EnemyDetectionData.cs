using UnityEngine;

namespace Unite
{
    public abstract class EnemyDetectionData : ScriptableObject, IDetectTarget
    {
        public abstract void StoreEnemyInfo(EnemyStateMachine enemy);
        public abstract bool IsTargetDetected(Transform target);
    }
}

