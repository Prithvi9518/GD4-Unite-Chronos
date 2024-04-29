using UnityEngine;

namespace Unite.BuffSystem
{
    /// <summary>
    /// Uses the provided buff selector to spawn a buff when a battle zone is completed.
    ///
    /// <seealso cref="WeightedRandomBuffSelector"/>
    /// </summary>
    public class BuffSpawnManager : MonoBehaviour
    {
        public static BuffSpawnManager Instance { get; private set; }
        
        [SerializeField]
        private BuffSelector buffSelector;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one instance present in the scene! Destroying current instance");
                Destroy(this);
            }

            Instance = this;
        }

        public void SpawnBuff(Transform spawnPosition)
        {
            GameObject buffToSpawn = buffSelector.SelectBuff();
            Instantiate(buffToSpawn, spawnPosition.position, Quaternion.identity);
        }
    }
}