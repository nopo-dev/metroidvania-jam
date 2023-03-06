using System.Collections.Generic;
using UnityEngine;

namespace FSM.Abstracts
{
    [CreateAssetMenu(menuName = "FSM/State")]
    public sealed class State : BaseState
    {
        public List<Action> Actions = new List<Action>();
        public List<Transition> Transitions = new List<Transition>();

        public override void Execute(BaseStateMachine machine)
        {
            foreach (var action in Actions)
            {
                action.Execute(machine);
            }

            foreach (var transition in Transitions)
            {
                transition.Execute(machine);
            }
        }
    }
}