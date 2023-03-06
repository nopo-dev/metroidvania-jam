using UnityEngine;

namespace FSM.Abstracts
{
    public abstract class BaseState : ScriptableObject
    {
        public abstract void Execute(BaseStateMachine machine);
    }
}