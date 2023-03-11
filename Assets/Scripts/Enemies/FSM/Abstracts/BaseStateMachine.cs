using System.Collections.Generic;
using UnityEngine;
using System;

namespace FSM.Abstracts
{
    public class BaseStateMachine : MonoBehaviour
    {
        [SerializeField] private BaseState _initialState;
        private Dictionary<Type, Component> _cachedComponents;
        private bool _locked;

        public BaseState CurrentState { get; set; }

        private void Awake()
        {
            CurrentState = _initialState;
            _locked = false;
            _cachedComponents = new Dictionary<Type, Component>();
        }

        private void Update()
        {
            if (_locked) { return; }

            CurrentState.Execute(this);
        }

        public void lockState()
        {
            _locked = true;
        }

        public void unlockState()
        {
            _locked = false;
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