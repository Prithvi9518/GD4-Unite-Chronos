using Unite.Enemies.Spawning;
using UnityEngine;

namespace Unite.EventSystem
{
    [CreateAssetMenu(fileName = "EnemySpawnerEvent", menuName = "Events/EnemySpawner")]
    public class EnemySpawnerEvent : ParameterisedGameEvent<EnemySpawner>
    {
    }
}