using UnityEngine;

namespace Unite.Enemies.Spawning
{
    public class BoxSpawnPositionProvider : MonoBehaviour, IProvideSpawnPosition
    {
        [SerializeField]
        private BoxCollider collider;

        [SerializeField] 
        private float offset;
        
        private Bounds bounds;

        private void Awake()
        {
            collider = GetComponent<BoxCollider>();
            bounds = collider.bounds;
        }

        public Vector3 GetSpawnPosition()
        {
            var spawnPos = new Vector3(
                Random.Range(bounds.min.x + offset, bounds.max.x - offset),
                transform.position.y,
                Random.Range(bounds.min.z + offset, bounds.max.z - offset)
            );

            return spawnPos;
        }
    }
}