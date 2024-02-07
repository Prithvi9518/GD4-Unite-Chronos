using FMODUnity;
using UnityEngine;

namespace Unite.SoundScripts
{
    public class FMODEvents : MonoBehaviour
    {
        public static FMODEvents Instance { get; private set; }

        [field: SerializeField]
        public EventReference MusicTransitionEvent { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("More than one instance of FMODEvents in the scene! Destroying current instance");
                Destroy(this);
            }

            Instance = this;
        }
    }
}