using UnityEngine;

namespace Unite.Enemies.Spawning
{
    public class BattleZone : MonoBehaviour
    {
        private EnemyWaveSpawner waveSpawner;

        private void Awake()
        {
            waveSpawner = GetComponent<EnemyWaveSpawner>();
        }

        public void StartBattle()
        {
            Debug.Log("Start Battle");
        }

    }
}