using System.Collections.Generic;
using Unite.Managers;
using UnityEngine;
using UnityEngine.Pool;

namespace Unite.Enemies
{
    public class EnemyPoolManager : MonoBehaviour
    {
        public static EnemyPoolManager Instance { get; private set; }
        
        [SerializeField]
        private EnemyData[] allEnemyTypes;

        private Dictionary<EnemyData, ObjectPool<Enemy>> enemyPools = new();

        public Dictionary<EnemyData, ObjectPool<Enemy>> EnemyPools => enemyPools;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            Instance = this;
            
            SetupPools();
        }

        private void SetupPools()
        {
            foreach (var enemyType in allEnemyTypes)
            {
                ObjectPool<Enemy> projectilePool = new ObjectPool<Enemy>(
                    () => CreateEnemy(enemyType),
                    actionOnGet:OnGetEnemyFromPool,
                    actionOnRelease:OnReleaseEnemyFromPool,
                    actionOnDestroy:OnDestroyEnemy);
                enemyPools.Add(enemyType, projectilePool);
            }
        }

        private Enemy CreateEnemy(EnemyData enemyType)
        {
            Enemy enemy = Instantiate(enemyType.EnemyPrefab);
            return enemy;
        }

        private void OnGetEnemyFromPool(Enemy enemy)
        {
            enemy.gameObject.SetActive(true);
            enemy.OnGetFromPool(GameManager.Instance.Player.transform);
        }
        
        private void OnReleaseEnemyFromPool(Enemy enemy)
        {
            enemy.gameObject.SetActive(false);
        }

        private void OnDestroyEnemy(Enemy enemy)
        {
            Destroy(enemy.gameObject);
        }
    }
}