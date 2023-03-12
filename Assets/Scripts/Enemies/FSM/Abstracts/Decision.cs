using UnityEngine;

namespace FSM.Abstracts
{
    public abstract class Decision : ScriptableObject
    {
        public bool blockable { get; protected set; } = true;

        public abstract bool Decide(BaseStateMachine machine);
    }
}