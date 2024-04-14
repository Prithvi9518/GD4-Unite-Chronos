using UnityEngine;

namespace Unite.Enemies.Spawning
{
    /// <summary>
    /// Any script that provides a position for enemies to spawn must implement this interface.
    ///
    /// <seealso cref="BoxSpawnPositionProvider"/>
    /// <seealso cref="MultiBoxSpawnPositionProvider"/>
    /// </summary>
    public interface IProvideSpawnPosition
    {
        public Vector3 GetSpawnPosition();
    }
}