using System;
using UnityEngine;

namespace Unite.SceneTransition
{
    public class SceneTransitionManager : MonoBehaviour
    {
        public static SceneTransitionManager Instance { get; private set; }

        public Action<Action> OnStartTransition;
        public Action<Action> OnFinishTransition;
        
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;
        }

        public void StartTransition(Action callback)
        {
            OnStartTransition?.Invoke(callback);
        }

        public void FinishTransition(Action callback)
        {
            OnFinishTransition?.Invoke(callback);
        }
    }
}