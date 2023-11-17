using UnityEngine;

namespace Unite.Enemies
{
    public class DebugEnemySetup : MonoBehaviour
    {
        [SerializeField] private EnemyData enemyData;
        [SerializeField] private Transform target;
        
        private Enemy enemy;

        private void Awake()
        {
            enemy = GetComponent<Enemy>();
        }

        private void Start()
        {
            enemyData.SetupEnemy(enemy, target);
        }
    }
}

