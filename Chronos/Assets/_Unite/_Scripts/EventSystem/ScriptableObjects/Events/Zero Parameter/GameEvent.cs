using System.Collections.Generic;
using UnityEngine;

namespace Unite.EventSystem
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Events/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        private List<GameEventListener> listeners = new();

        [ContextMenu("Raise Event")]
        public virtual void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(GameEventListener listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            listeners.Remove(listener);
        }
    }
}

