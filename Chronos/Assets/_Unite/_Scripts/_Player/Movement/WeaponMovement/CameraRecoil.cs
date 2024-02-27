using UnityEngine;

namespace Unite.Player
{
    public class CameraRecoil : MonoBehaviour
    {
        [SerializeField]
        private Vector3 hipFireRecoil;

        [SerializeField]
        private float snappiness;
        
        [SerializeField]
        private float returnSpeed;

        private Vector3 currentRotation;
        private Vector3 targetRotation;

        private void FixedUpdate()
        {
            targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
            currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
            transform.localRotation = Quaternion.Euler(currentRotation);
        }

        public void ApplyRecoil()
        {
            targetRotation += new Vector3(-hipFireRecoil.x,
                Random.Range(-hipFireRecoil.y, hipFireRecoil.y),
                Random.Range(-hipFireRecoil.z, hipFireRecoil.z));
        }
    }
}