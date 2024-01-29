using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Unite.VFXScripts
{
    public class DamageFlashVFX : MonoBehaviour
    {
        [SerializeField] 
        private Volume volume;

        [SerializeField]
        private float flashTimeInSeconds = 0.4f;

        [SerializeField]
        private float intensityDecreaseRate = 0.01f;

        [SerializeField]
        private float fadeOutTickRate = 0.1f;

        [Header("Vignette Adjustment Params")]
        [SerializeField]
        private Color color;

        [SerializeField]
        private float intensity;

        [SerializeField]
        private float smoothness = 1f;

        private Vignette vignette;
        private Color defaultColor;
        private float defaultIntensity, defaultSmoothness;

        private WaitForSeconds waitAfterFlashStart;
        private WaitForSeconds waitWhileFadeOut;

        private void Awake()
        {
            waitAfterFlashStart = new WaitForSeconds(flashTimeInSeconds);
            waitWhileFadeOut = new WaitForSeconds(fadeOutTickRate);
        }

        private void Start()
        {
            if (volume.profile.TryGet(out Vignette v))
            {
                vignette = v;

                defaultColor = v.color.value;
                defaultIntensity = v.intensity.value;
                defaultSmoothness = v.smoothness.value;
            }

            vignette.active = true;
            vignette.color.overrideState = true;
            vignette.intensity.overrideState = true;
            vignette.smoothness.overrideState = true;
        }

        public void HandleDamageFlash()
        {
            StartCoroutine(DamageFlashCoroutine());
        }

        private IEnumerator DamageFlashCoroutine()
        {
            vignette.color.value = color;
            vignette.intensity.value = intensity;
            vignette.smoothness.value = smoothness;

            yield return waitAfterFlashStart;

            while (vignette.intensity.value > defaultIntensity)
            {
                vignette.intensity.value -= intensityDecreaseRate;
                yield return waitWhileFadeOut;
            }

            vignette.color.value = defaultColor;
            vignette.intensity.value = defaultIntensity;
            vignette.smoothness.value = defaultSmoothness;
        }
    }
}