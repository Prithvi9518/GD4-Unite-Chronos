using UnityEngine;

namespace Unite.Enemies.Spawning
{
    public class TransformSpawnPositionProvider : MonoBehaviour, IProvideSpawnPosition
    {
        [SerializeField]
        private Transform[] spawnPositions;

        public Vector3 GetSpawnPosition()
        {
            int index = Random.Range(0, spawnPositions.Length);
            
            return spawnPositions[index].position;
        }
    }
}