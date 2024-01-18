using System.Collections.Generic;
using UnityEngine;

namespace Unite.EventSystem
{
    public abstract class ParameterisedGameEvent<T> : ScriptableObject
    {
        private List<ParameterisedGameEventListener<T>> listeners = new();

        public virtual void Raise(T data)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(data);
            }
        }

        public void RegisterListener(ParameterisedGameEventListener<T> listener)
        {
            listeners.Add(listener);
        }

        public void UnregisterListener(ParameterisedGameEventListener<T> listener)
        {
            listeners.Remove(listener);
        }
    }
}