using UnityEngine;

namespace FSM.Abstracts
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(BaseStateMachine machine);
    }
}