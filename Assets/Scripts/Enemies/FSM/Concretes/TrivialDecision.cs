using FSM.Abstracts;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Decisions/Trivial")]
    public class TrivialDecision : Decision
    {
        public override bool Decide(BaseStateMachine machine)
        {
            return true;
        }
    }
}