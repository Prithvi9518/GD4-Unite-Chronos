using Unite.TimeStop;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Unite.VFXScripts
{
    public class TimeStopVFX : MonoBehaviour, ITimeStopSubscriber
    {
        [SerializeField]
        private Volume volume;

        [Header("Color Adjustment Params")]
        [SerializeField]
        private Color color;

        [SerializeField]
        private float contrast;

        [SerializeField]
        private float hueShift;

        [SerializeField]
        private float saturation;

        [SerializeField]
        private float postExposure;

        [Header("Bloom Params")]
        [SerializeField]
        private float bloomThreshold;

        [SerializeField]
        private float bloomIntensity;

        [SerializeField]
        private Color bloomTint;

        [SerializeField]
        private float bloomDirtIntensity;

        [Header("Vignette Params")]
        [SerializeField]
        private float vignetteIntensity;

        [SerializeField]
        private float vignetteSmoothness;

        [Header("Chromatic Aberration Params")]
        [SerializeField]
        private float chromaticAberrationIntensity;

        private ColorAdjustments colorAdjustments;
        private Bloom bloom;
        private Vignette vignette;
        private ChromaticAberration chromaticAberration;

        private Color defaultColor;
        private float defaultContrast, defaultHueShift, defaultSaturation, defaultPostExposure;
        private Color defaultBloomTint;
        private float defaultBloomThreshold, defaultBloomIntensity, defaultBloomDirtIntensity;
        private float defaultVignetteIntensity, defaultVignetteSmoothness;
        private float defaultChromaticAberrationIntensity;

        private void Start()
        {
            if(volume.profile.TryGet<ColorAdjustments>(out ColorAdjustments ca))
            {
                colorAdjustments = ca;

                defaultColor = ca.colorFilter.value;
                defaultContrast = ca.contrast.value;
                defaultHueShift = ca.hueShift.value;
                defaultSaturation = ca.saturation.value;
                defaultPostExposure = ca.postExposure.value;
            }
            if (volume.profile.TryGet<Bloom>(out Bloom bloomComponent))
            {
                bloom = bloomComponent;

                defaultBloomThreshold = bloomComponent.threshold.value;
                defaultBloomIntensity = bloomComponent.intensity.value;
                defaultBloomTint = bloomComponent.tint.value;
                defaultBloomDirtIntensity = bloomComponent.dirtIntensity.value;
            }
            if (volume.profile.TryGet<Vignette>(out Vignette vignetteComponent))
            {
                vignette = vignetteComponent;

                defaultVignetteIntensity = vignetteComponent.intensity.value;
                defaultVignetteSmoothness = vignetteComponent.smoothness.value;
            }
            if (volume.profile.TryGet<ChromaticAberration>(out ChromaticAberration chromaticAberrationComponent))
            {
                chromaticAberration = chromaticAberrationComponent;

                defaultChromaticAberrationIntensity = chromaticAberrationComponent.intensity.value;
            }
            colorAdjustments.active = true;
            colorAdjustments.colorFilter.overrideState = true;
            colorAdjustments.hueShift.overrideState = true;
            colorAdjustments.contrast.overrideState = true;
            colorAdjustments.postExposure.overrideState = true;
            colorAdjustments.saturation.overrideState = true;

            bloom.threshold.overrideState = true;
            bloom.intensity.overrideState = true;
            bloom.tint.overrideState = true;
            bloom.dirtIntensity.overrideState = true;

            vignette.intensity.overrideState = true;
            vignette.smoothness.overrideState = true;

            chromaticAberration.intensity.overrideState = true;
        }

        public void HandleTimeStopEvent(bool isTimeStopped)
        {
            if(isTimeStopped)
            {
                colorAdjustments.colorFilter.value = color;
                colorAdjustments.hueShift.value = hueShift;
                colorAdjustments.contrast.value = contrast;
                colorAdjustments.postExposure.value = postExposure;
                colorAdjustments.saturation.value = saturation;

                bloom.threshold.value = bloomThreshold;
                bloom.intensity.value = bloomIntensity;
                bloom.tint.value = bloomTint;
                bloom.dirtIntensity.value = bloomDirtIntensity;

                vignette.intensity.value = vignetteIntensity;
                vignette.smoothness.value = vignetteSmoothness;

                chromaticAberration.intensity.value = chromaticAberrationIntensity;
            }
            else
            {
                colorAdjustments.colorFilter.value = defaultColor;
                colorAdjustments.hueShift.value = defaultHueShift;
                colorAdjustments.contrast.value = defaultContrast;
                colorAdjustments.postExposure.value = defaultPostExposure;
                colorAdjustments.saturation.value = defaultSaturation;

                bloom.threshold.value = defaultBloomThreshold;
                bloom.intensity.value = defaultBloomIntensity;
                bloom.tint.value = defaultBloomTint;
                bloom.dirtIntensity.value = defaultBloomDirtIntensity;

                vignette.intensity.value = defaultVignetteIntensity;
                vignette.smoothness.value = defaultVignetteSmoothness;

                chromaticAberration.intensity.value = defaultChromaticAberrationIntensity;
            }
        }
    }
}

