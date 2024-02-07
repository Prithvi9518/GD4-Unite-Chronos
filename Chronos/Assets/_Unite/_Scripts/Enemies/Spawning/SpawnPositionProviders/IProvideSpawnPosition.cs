using UnityEngine;

namespace Unite.Enemies.Spawning
{
    public interface IProvideSpawnPosition
    {
        public Vector3 GetSpawnPosition();
    }
}