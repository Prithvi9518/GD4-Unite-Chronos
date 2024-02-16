using UnityEngine;

namespace Unite.Player
{
    public class MoveCamera : MonoBehaviour
    {
        [SerializeField]
        private Transform follow;

        private void Update()
        {
            transform.position = follow.position;
        }
    }
}