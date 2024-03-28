using System;
using Unite.EventSystem;
using UnityEngine;

namespace Unite.SceneTransition
{
    public class SceneTransitionManager : MonoBehaviour
    {
        public static SceneTransitionManager Instance { get; private set; }

        [SerializeField] 
        private GameEvent onTransitionStart;

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
            onTransitionStart.Raise();
            OnStartTransition?.Invoke(callback);
        }

        public void FinishTransition(Action callback)
        {
            OnFinishTransition?.Invoke(callback);
        }
    }
}