namespace Unite.BuffSystem.ScriptableObjects
{
    /// <summary>
    /// Runtime configuration of a buff, used to update and track the number
    /// of times it has been spawned so far, and whether it can still be spawned.
    /// </summary>
    public class BuffSpawnConfig
    {
        private BuffSpawnSO buffSpawnSO;

        private bool canSpawn;
        private int numTimesSpawned;

        public bool CanSpawn => canSpawn;
        public BuffSpawnSO BuffSpawn => buffSpawnSO;

        public BuffSpawnConfig(BuffSpawnSO buffSpawn)
        {
            buffSpawnSO = buffSpawn;
            canSpawn = true;
            numTimesSpawned = 0;
        }

        /// <summary>
        /// Called after a buff has been spawned to check and update whether the buff
        /// can be spawned the next time.
        /// </summary>
        public void UpdateCanSpawn()
        {
            if (buffSpawnSO.UnlimitedSpawning)
            {
                canSpawn = true;
                return;
            }

            numTimesSpawned++;
            canSpawn = numTimesSpawned < buffSpawnSO.SpawnLimit;
        }
    }
}