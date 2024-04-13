using UnityEngine;

namespace Unite.Player
{
    /// <summary>
    /// Handles camera recoil when the player's gun is shot.
    /// Reference: Tutorial by Swindle Creative - https://youtu.be/6hyQ2rPkMDY?si=S3wkjWRGJDeajzgl
    ///
    /// <seealso cref="WeaponRecoil"/>
    /// </summary>
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

        /// <summary>
        /// Method is called as a response to an event when the gun is shot
        /// </summary>
        public void ApplyRecoil()
        {
            targetRotation += new Vector3(-hipFireRecoil.x,
                Random.Range(-hipFireRecoil.y, hipFireRecoil.y),
                Random.Range(-hipFireRecoil.z, hipFireRecoil.z));
        }
    }
}