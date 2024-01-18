using Unite.Enemies;
using UnityEngine;

namespace Unite.EventSystem
{
    [CreateAssetMenu(fileName = "EnemyEvent", menuName = "Events/Enemy Event")]
    public class EnemyEvent : ParameterisedGameEvent<Enemy>
    {
    }
}