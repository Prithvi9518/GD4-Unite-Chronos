using Unite.Managers;
using UnityEngine;

namespace Unite.SceneTransition
{
    public class TestSceneTransitionTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Player.Player player)) return;
            GameManager.Instance.SwitchToNextLevel();
        }
    }
}