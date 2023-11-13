using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Unite
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

        private ColorAdjustments colorAdjustments;

        private Color defaultColor;
        private float defaultContrast, defaultHueShift, defaultSaturation, defaultPostExposure;

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

            colorAdjustments.active = true;
            colorAdjustments.colorFilter.overrideState = true;
            colorAdjustments.hueShift.overrideState = true;
            colorAdjustments.contrast.overrideState = true;
            colorAdjustments.postExposure.overrideState = true;
            colorAdjustments.saturation.overrideState = true;
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
            }
            else
            {
                colorAdjustments.colorFilter.value = defaultColor;
                colorAdjustments.hueShift.value = defaultHueShift;
                colorAdjustments.contrast.value = defaultContrast;
                colorAdjustments.postExposure.value = defaultPostExposure;
                colorAdjustments.saturation.value = defaultSaturation;
            }
        }
    }
}

