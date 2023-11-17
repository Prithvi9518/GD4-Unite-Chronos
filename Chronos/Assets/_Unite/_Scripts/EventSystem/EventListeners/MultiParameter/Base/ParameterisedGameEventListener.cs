using UnityEngine;
using UnityEngine.Events;

namespace Unite.EventSystem
{
    public class ParameterisedGameEventListener<T> : MonoBehaviour
    {
        [SerializeField] private string description;

        [SerializeField] private ParameterisedGameEvent<T> gameEvent;

        [SerializeField] private UnityEvent<T> response;

        private void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            gameEvent.UnregisterListener(this);
        }

        public virtual void OnEventRaised(T data)
        {
            response?.Invoke(data);
        }
    }
}