using UnityEditor;
using UnityEngine;

namespace Unite
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefabToSpawn;

        [SerializeField]
        private Terrain terrain;

        [Header("Raycast Settings")]
        [SerializeField]
        private int density;

        [SerializeField]
        private float rayHeight;

        [Header("Prefab Variation Settings")]
        [SerializeField, Range(0, 1)]
        private float rotateTowardsNormal;

        [SerializeField]
        private Vector2 rotationRange;

        [SerializeField]
        private Vector3 minScale;

        [SerializeField]
        private Vector3 maxScale;

        private Vector3 terrainSize;
        private Vector3 terrainPos;

        public void SpawnObjects()
        {
            terrainSize = terrain.terrainData.size;
            terrainPos = terrain.transform.position;

            for (int i = 0; i < density; i++)
            {
                float sampleX = Random.Range(terrainPos.x, terrainPos.x + terrainSize.x);
                float sampleZ = Random.Range(terrainPos.z, terrainPos.z + terrainSize.z);

                Vector3 rayOrigin = new Vector3(sampleX, rayHeight, sampleZ);

                RaycastHit hit;

                if (!Physics.Raycast(rayOrigin, Vector3.down, out hit))
                    continue;

                GameObject spawnedObject = Instantiate(prefabToSpawn, transform);

                spawnedObject.transform.position = hit.point;

                spawnedObject.transform.Rotate(
                    Vector3.up,
                    Random.Range(rotationRange.x, rotationRange.y),
                    Space.Self
                );

                Quaternion initialObjectRotation = spawnedObject.transform.rotation;
                spawnedObject.transform.rotation = Quaternion.Lerp(
                    initialObjectRotation,
                    initialObjectRotation * Quaternion.FromToRotation(spawnedObject.transform.up, hit.normal),
                    rotateTowardsNormal
                );

                spawnedObject.transform.localScale = new Vector3(
                    Random.Range(minScale.x, maxScale.x),
                    Random.Range(minScale.y, maxScale.y),
                    Random.Range(minScale.y, maxScale.y)
                );
            }
        }

        public void Clear()
        {
            while (transform.childCount > 0)
            {
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(terrainPos, terrainPos + terrainSize);
        }
    }
}