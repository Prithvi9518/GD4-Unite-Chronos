using Cinemachine;
using UnityEngine;

namespace Unite.VFXScripts
{
    public class CinemachineShake : MonoBehaviour
    {
        [Header("Shooting Camera Shake Settings")]
        [SerializeField]
        private float intensity;
        
        [SerializeField]
        private float shakeTimeInMs;
        
        private CinemachineVirtualCamera cinemachineVirtualCamera;
        private CinemachineBasicMultiChannelPerlin basicMultiChannelPerlin;
        private float shakeTimer;

        private void Awake()
        {
            cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
            basicMultiChannelPerlin =
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        public void ShakeCamera()
        {
            basicMultiChannelPerlin.m_AmplitudeGain = intensity;
            shakeTimer = shakeTimeInMs;
        }

        private void Update()
        {
            if (!(shakeTimer > 0f)) return;
            
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                basicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}

