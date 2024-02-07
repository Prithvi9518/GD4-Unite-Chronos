﻿using UnityEngine;

namespace Unite.UI
{
    public class TriggerWorldSpaceText : MonoBehaviour
    {
        [SerializeField]
        private Transform textTransform;

        private Camera cam;

        private void Awake()
        {
            textTransform.gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Player.Player player)) return;
            
            if(cam == null)
                cam = Camera.main;
            
            textTransform.gameObject.SetActive(true);

            if (cam == null) return;
            textTransform.parent.rotation = cam.transform.rotation;
        }

        private void OnTriggerStay(Collider other)
        {
            if (!other.TryGetComponent(out Player.Player player)) return;
            textTransform.parent.rotation = cam.transform.rotation;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out Player.Player player)) return;
            textTransform.gameObject.SetActive(false);
        }
    }
}