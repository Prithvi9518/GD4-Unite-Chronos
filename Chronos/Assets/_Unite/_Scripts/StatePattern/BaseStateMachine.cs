using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unite.StatePattern
{
    public class BaseStateMachine : MonoBehaviour
    {
        private State startingState;

        // Dummy state used to remain in the same state if needed
        private State remainState;

        private State currentState;
        
        private Dictionary<Type, Component> cachedComponents;

        protected virtual void Awake()
        {
            cachedComponents = new Dictionary<Type, Component>();
        }

        private void Update()
        {
            currentState.UpdateState(this);
        }
        
        public new T GetComponent<T>() where T : Component
        {
            if(cachedComponents.ContainsKey(typeof(T)))
                return cachedComponents[typeof(T)] as T;

            var component = base.GetComponent<T>();
            if(component != null)
            {
                cachedComponents.Add(typeof(T), component);
            }
            return component;
        }
        
        public void SetCurrentState(State state)
        {
            if (state == remainState) return;

            currentState.ExitState(this);
            currentState = state;
            currentState.EnterState(this);
        }
        
        public void PerformSetup(State start, State remain)
        {
            startingState = start;
            remainState = remain;

            currentState = startingState;
            currentState.EnterState(this);
        }
        
        public void TriggerStateEvent(StateEvent stateEvent)
        {
            currentState.CheckEventTransitions(this, stateEvent);
        }
    }
}