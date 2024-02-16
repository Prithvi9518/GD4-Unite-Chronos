using Unite.Core.Game;
using UnityEngine;

namespace Unite.Bootstrap
{
    public class Bootstrapper : MonoBehaviour
    {
        private Player.Player player;

        private void Start()
        {
            DontDestroyOnLoad(this);
        }

        public void HandlePlayerReadyEvent(Player.Player p)
        {
            player = p;
            CheckAndDoBootstrap();
        }

        private void CheckAndDoBootstrap()
        {
            if (player == null) return;
            Managers.GameManager.Instance.Initialize(player);
            Managers.GameManager.Instance.SetGameState(GameState.Start);
        }
    }
}