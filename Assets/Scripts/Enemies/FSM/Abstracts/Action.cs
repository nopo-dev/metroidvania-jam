using UnityEngine;

namespace FSM.Abstracts
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Execute(BaseStateMachine machine);
    }
}