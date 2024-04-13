using UnityEngine;
using Random = UnityEngine.Random;

namespace Unite.Player
{
    /// <summary>
    /// Used to handle kickback and rotation of the player's gun when it is shot.
    /// Reference: Tutorial by Swindle Creative - https://youtu.be/6hyQ2rPkMDY?si=S3wkjWRGJDeajzgl
    /// 
    /// <seealso cref="CameraRecoil"/>
    /// </summary>
    public class WeaponRecoil : MonoBehaviour
    {
        [Header("Reference Points")] 
        [SerializeField]
        private Transform recoilPosition;
        [SerializeField]
        private Transform rotationPoint;

        [Header("Speed Settings")] 
        [SerializeField]
        private float positionalRecoilSpeed = 8f;
        [SerializeField]
        private float rotationalRecoilSpeed = 8f;

        [SerializeField]
        private float positionalReturnSpeed = 18f;
        [SerializeField]
        private float rotationalReturnSpeed = 38f;

        [Header("Amount Settings")]
        [SerializeField]
        private Vector3 recoilRotation = new Vector3(10, 5, 7);
        [SerializeField]
        private Vector3 recoilKickback = new Vector3(0.015f, 0f, -0.2f);

        private Vector3 rotationalRecoil;
        private Vector3 positionalRecoil;
        private Vector3 rotation;

        private Vector3 startingPosition;
        private Vector3 startingRotation;

        private void Awake()
        {
            startingPosition = recoilPosition.localPosition;
            startingRotation = rotationPoint.localRotation.eulerAngles;
        }

        private void FixedUpdate()
        {
            rotationalRecoil = Vector3.Lerp(rotationalRecoil, startingRotation, rotationalReturnSpeed * Time.deltaTime);
            positionalRecoil = Vector3.Lerp(positionalRecoil, startingPosition, positionalReturnSpeed * Time.deltaTime);

            recoilPosition.localPosition = Vector3.Slerp(recoilPosition.localPosition, positionalRecoil,
                positionalRecoilSpeed * Time.fixedDeltaTime);
            
            rotation = Vector3.Slerp(rotation, rotationalRecoil, rotationalRecoilSpeed * Time.fixedDeltaTime);
            rotationPoint.localRotation = Quaternion.Euler(rotation);
        }

        public void ApplyRecoil()
        {
            rotationalRecoil += new Vector3(
                -recoilRotation.x,
                Random.Range(-recoilRotation.y, recoilRotation.y),
                Random.Range(-recoilRotation.z, recoilRotation.z)
            );
            
            positionalRecoil += new Vector3(
                Random.Range(-recoilKickback.x, recoilKickback.x),
                Random.Range(-recoilKickback.y, recoilKickback.y),
                recoilKickback.z
            );
        }
    }
}