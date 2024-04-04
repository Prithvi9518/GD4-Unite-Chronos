using Unite.BuffSystem;
using Unite.Enemies.Spawning;
using Unite.EventSystem;
using Unite.VFXScripts;
using UnityEngine;

namespace Unite.BattleSystem
{
    public class BattleZone : MonoBehaviour
    {
        [SerializeField]
        private string displayName;
        
        [SerializeField] 
        private EnemyWave[] enemyWaves;

        [SerializeField]
        private BattleZoneBarrier barrier;

        [SerializeField] 
        private GameObject visual;
        
        [SerializeField]
        private Transform buffSpawnPosition;

        [Header("Force Field VFX:")]
        [SerializeField] 
        private ForceFieldController forceFieldController;

        [Header("Events for starting, progressing and finishing a battle:")]
        [SerializeField]
        private BattleZoneInfoEvent onStartBattle;
        
        [SerializeField]
        private BattleZoneInfoEvent onMoveToNextWave;

        [SerializeField]
        private GameEvent[] onFinishBattleEvents;

        [Header("Events for analytics:")]
        [SerializeField]
        private BattleZoneInfoEvent onStartBattleUpdateAnalytics;
        
        [SerializeField]
        private BattleFinishedInfoEvent onFinishBattleUpdateAnalytics;
        
        private BattleState battleState;
        private EnemyWaveSpawner waveSpawner;
        private IProvideSpawnPosition spawnPositionProvider;
        private Transform playerTransform;
        private int currentWaveIndex;
        
        private float timeBattleStarted;
        private float timeBattleFinished;

        public string DisplayName => displayName;
        public BattleZoneBarrier Barrier => barrier;
        public BattleState BattleState => battleState;
        public ForceFieldController ForceFieldController => forceFieldController;

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
            if (!AreAllEnemiesDeadInCurrentWave()) return;
            
            if (currentWaveIndex + 1 >= enemyWaves.Length)
                EndBattle();
            else
                SpawnNextWave();
        }

        public void StartBattle(Player.Player player)
        {
            if (battleState != BattleState.Idle) return;
            
            playerTransform = player.transform;
            waveSpawner.SpawnEnemies(enemyWaves[currentWaveIndex], spawnPositionProvider, playerTransform);
            battleState = BattleState.Active;
            
            BattleTracker.Instance.RegisterBattleZone(this);

            timeBattleStarted = Time.realtimeSinceStartup;

            BattleZoneInfo info = new BattleZoneInfo(displayName, GetCurrentWave());
            onStartBattle.Raise(info);
            onStartBattleUpdateAnalytics.Raise(info);
        }

        public int GetCurrentWave()
        {
            if (battleState != BattleState.Active) return -1;
            return currentWaveIndex + 1;
        }

        private bool AreAllEnemiesDeadInCurrentWave()
        {
            return waveSpawner.NumEnemiesAlive <= 0;
        }

        private void SpawnNextWave()
        {
            currentWaveIndex++;
            waveSpawner.SpawnEnemies(enemyWaves[currentWaveIndex], spawnPositionProvider, playerTransform);
            
            onMoveToNextWave.Raise(new BattleZoneInfo(displayName, GetCurrentWave()));
        }

        private void EndBattle()
        {
            battleState = BattleState.End;
            BuffSpawnManager.Instance.SpawnBuff(buffSpawnPosition);
            barrier.ToggleBarrierColliders(false);

            timeBattleFinished = Time.realtimeSinceStartup;
            float timeDifference = timeBattleFinished - timeBattleStarted;

            foreach (var e in onFinishBattleEvents)
            {
                e.Raise();
            }
            
            onFinishBattleUpdateAnalytics.Raise(new BattleFinishedInfo(displayName, timeDifference));
            
            visual.SetActive(false);
        }
    }
}