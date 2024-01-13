using UnityEngine;

namespace Unite.BuffSystem
{
    public class BuffSpawner : MonoBehaviour
    {
        [SerializeField]
        private BuffSelector buffSelector;

        [SerializeField]
        private Transform spawnPosition;

        public void SpawnBuff()
        {
            GameObject buffToSpawn = buffSelector.SelectBuff();
            Instantiate(buffToSpawn, spawnPosition.position, Quaternion.identity);
        }
    }
}