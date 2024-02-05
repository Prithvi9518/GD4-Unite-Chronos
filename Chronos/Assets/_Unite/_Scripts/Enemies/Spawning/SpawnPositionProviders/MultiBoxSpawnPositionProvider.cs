using UnityEngine;

namespace Unite.Enemies.Spawning
{
    public class MultiBoxSpawnPositionProvider : MonoBehaviour, IProvideSpawnPosition
    {
        [SerializeField]
        private BoxCollider[] colliders;

        [SerializeField] 
        private float offset;

        [SerializeField] 
        private float navmeshHitOffset;

        public Vector3 GetSpawnPosition()
        {
            var randomCollider = colliders[Random.Range(0, colliders.Length)];
            var bounds = randomCollider.bounds;
            
            var spawnPos = new Vector3(
                Random.Range(bounds.min.x + offset, bounds.max.x - offset),
                transform.position.y + navmeshHitOffset,
                Random.Range(bounds.min.z + offset, bounds.max.z - offset)
            );

            return spawnPos;
        }
    }
}