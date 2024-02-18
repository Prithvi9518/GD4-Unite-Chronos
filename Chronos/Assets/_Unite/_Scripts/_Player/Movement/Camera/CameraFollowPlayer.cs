using UnityEngine;

namespace Unite.Player
{
    public class CameraFollowPlayer : MonoBehaviour
    {
        private Transform follow;
        private bool startFollow;
        
        public void InitializeFollow()
        {
            Debug.Log("CameraFollowPlayer - InitializeFollow()");
            follow = Managers.GameManager.Instance.Player.CameraRoot;
            startFollow = true;
        }

        private void Update()
        {
            if (!startFollow) return;
            transform.position = follow.position;
        }
    }
}