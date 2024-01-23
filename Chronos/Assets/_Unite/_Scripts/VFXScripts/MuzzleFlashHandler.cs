using System.Collections;
using UnityEngine;

namespace Unite.VFXScripts
{
    public class MuzzleFlashHandler : MonoBehaviour
    {
        [SerializeField]
        private Transform muzzleFlash;

        [SerializeField]
        private float disableTimeInSeconds;

        private WaitForSeconds wait;

        private void Awake()
        {
            muzzleFlash.gameObject.SetActive(false);
            wait = new WaitForSeconds(disableTimeInSeconds);
        }

        public void PlayMuzzleFlash()
        {
            StartCoroutine(MuzzleFlashCoroutine());
        }

        private IEnumerator MuzzleFlashCoroutine()
        {
            muzzleFlash.gameObject.SetActive(true);
            yield return wait;
            muzzleFlash.gameObject.SetActive(false);
        }
    }
}