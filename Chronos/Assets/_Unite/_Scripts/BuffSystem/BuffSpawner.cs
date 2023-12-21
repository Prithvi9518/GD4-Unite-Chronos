using UnityEngine;

namespace Unite.BuffSystem
{
    public class BuffSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject buffToSpawn;

        [SerializeField]
        private Transform spawnPosition;

        public void SpawnBuff()
        {
            Instantiate(buffToSpawn, spawnPosition.position, Quaternion.identity);
        }
    }
}