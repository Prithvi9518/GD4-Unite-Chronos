using UnityEngine;
using UnityEngine.Events;

namespace Unite.EventSystem
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] 
        private string description;

        [SerializeField] 
        private GameEvent gameEvent;

        [SerializeField] 
        private UnityEvent response;

        private void OnEnable()
        {
            gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            gameEvent.UnregisterListener(this);
        }

        public virtual void OnEventRaised()
        {
            response?.Invoke();
        }
    }
}
