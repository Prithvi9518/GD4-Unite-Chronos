using UnityEngine;

namespace Unite.Enemies.Spawning
{
    public class BattleZone : MonoBehaviour
    {
        private BattleState battleState;
        private EnemyWaveSpawner waveSpawner;
        private IProvideSpawnPosition spawnPositionProvider;
        
        private void Awake()
        {
            battleState = BattleState.Idle;
            waveSpawner = GetComponent<EnemyWaveSpawner>();
            spawnPositionProvider = GetComponent<IProvideSpawnPosition>();
        }

        public void StartBattle(Player.Player player)
        {
            if (battleState != BattleState.Idle) return;
            
            Debug.Log("Start Battle");
            waveSpawner.SpawnEnemies(spawnPositionProvider, player.transform);
            battleState = BattleState.Active;
        }

    }
}