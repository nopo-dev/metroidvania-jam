using System.Collections.Generic;
using UnityEngine;
using System;

namespace FSM.Abstracts
{
    public class BaseStateMachine : MonoBehaviour
    {
        [SerializeField] private BaseState _initialState;
        private Dictionary<Type, Component> _cachedComponents;
        [HideInInspector] public bool actionLocked { get; private set; }
        [HideInInspector] public bool transitionLocked { get; private set; }

        [SerializeField] public BaseState CurrentState;

        private void Awake()
        {
            if (_initialState == null)
            {
                Debug.Log("BaseStateMachine - Initial state cannot be null!");
            }
            CurrentState = _initialState;
            actionLocked = false;
            transitionLocked = false;
            _cachedComponents = new Dictionary<Type, Component>();
        }

        private void Update()
        {
            if (PauseControl.gameIsPaused) { return; }
            CurrentState.Execute(this);
        }

        public void lockActions()
        {
            actionLocked = true;
        }

        public void unlockActions()
        {
            actionLocked = false;
        }

        public void lockTransitions()
        {
            transitionLocked = true;
        }

        public void unlockTransitions()
        {
            transitionLocked = false;
        }

        public void lockState()
        {
            lockTransitions();
            lockActions();
        }

        public void unlockState()
        {
            unlockTransitions();
            unlockActions();
        }

        public new T GetComponent<T>() where T : Component
        {
            if (_cachedComponents.ContainsKey(typeof(T)))
            {
                return _cachedComponents[typeof(T)] as T;
            }   

            var component = base.GetComponent<T>();
            if (component != null)
            {
                _cachedComponents.Add(typeof(T), component);
            }
            return component;
        }
    }
}