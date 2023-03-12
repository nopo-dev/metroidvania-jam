using UnityEngine;

namespace FSM.Abstracts
{
    public abstract class Action : ScriptableObject
    {
        public bool blockable { get; protected set; } = true;

        public abstract void Execute(BaseStateMachine machine);
    }
}