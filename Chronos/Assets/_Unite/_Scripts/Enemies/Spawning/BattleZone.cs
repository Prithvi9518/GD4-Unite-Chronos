using UnityEngine;

namespace Unite.Enemies.Spawning
{
    public class BattleZone : MonoBehaviour
    {
        private BattleState battleState;
        private EnemyWaveSpawner waveSpawner;
        
        private void Awake()
        {
            battleState = BattleState.Idle;
            waveSpawner = GetComponent<EnemyWaveSpawner>();
        }

        public void StartBattle(Player.Player player)
        {
            if (battleState != BattleState.Idle) return;
            
            Debug.Log("Start Battle");
            waveSpawner.SpawnEnemies(transform.position, player.transform);
            battleState = BattleState.Active;
        }

    }
}