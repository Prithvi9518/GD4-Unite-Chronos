namespace Unite.BuffSystem.ScriptableObjects
{
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