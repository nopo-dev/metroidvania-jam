using UnityEngine;

namespace FSM.Abstracts
{
    [CreateAssetMenu(menuName = "FSM/Transition")]
    public sealed class Transition : ScriptableObject
    {
        public Decision decision;
        public BaseState TrueState;
        public BaseState FalseState;

        public void Execute(BaseStateMachine machine)
        {
            if (decision.Decide(machine))
            {
                if (!(TrueState is RemainInState))
                {
                    machine.CurrentState = TrueState;
                    machine.unlockState();
                }
            }
            else if (!(FalseState is RemainInState))
            {
                machine.CurrentState = FalseState;
                machine.unlockState();
            }
            // RemainInState should not unlock state
        }
    }
}