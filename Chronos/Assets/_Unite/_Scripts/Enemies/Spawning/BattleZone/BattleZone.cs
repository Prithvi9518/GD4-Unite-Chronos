using UnityEngine;

namespace Unite.Enemies.Spawning
{
    public class BattleZone : MonoBehaviour
    {
        [SerializeField] 
        private EnemyWave[] enemyWaves;
        
        private BattleState battleState;
        private EnemyWaveSpawner waveSpawner;
        private IProvideSpawnPosition spawnPositionProvider;
        private Transform playerTransform;
        private int currentWaveIndex;
        
        private void Awake()
        {
            battleState = BattleState.Idle;
            waveSpawner = GetComponent<EnemyWaveSpawner>();
            spawnPositionProvider = GetComponent<IProvideSpawnPosition>();

            currentWaveIndex = 0;
        }

        private void Update()
        {
            if(battleState != BattleState.Active) return;
            if (AreAllEnemiesDeadInCurrentWave())
            {
                Debug.Log($"All enemies dead in wave {currentWaveIndex + 1}");

                if (currentWaveIndex + 1 >= enemyWaves.Length)
                    EndBattle();
                else
                    SpawnNextWave();
            }
        }

        public void StartBattle(Player.Player player)
        {
            if (battleState != BattleState.Idle) return;
            
            Debug.Log("Start Battle");
            playerTransform = player.transform;
            waveSpawner.SpawnEnemies(enemyWaves[currentWaveIndex], spawnPositionProvider, playerTransform);
            battleState = BattleState.Active;
        }

        private bool AreAllEnemiesDeadInCurrentWave()
        {
            return waveSpawner.NumEnemiesAlive <= 0;
        }

        private void SpawnNextWave()
        {
            currentWaveIndex++;
            waveSpawner.SpawnEnemies(enemyWaves[currentWaveIndex], spawnPositionProvider, playerTransform);
        }

        private void EndBattle()
        {
            battleState = BattleState.End;
            Debug.Log("Battle ended");
        }
    }
}