using System.Collections.Generic;
using UnityEngine;

namespace Unite.EventSystem
{
    public abstract class ParameterisedGameEvent<T> : ScriptableObject
    {
        private HashSet<ParameterisedGameEventListener<T>> listeners = new HashSet<ParameterisedGameEventListener<T>>();

        public virtual void Raise(T data)
        {
            foreach (var listener in listeners)
            {
                listener.OnEventRaised(data);
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