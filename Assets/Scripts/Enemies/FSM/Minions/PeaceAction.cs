using FSM.Abstracts;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Actions/Peace")]
    public class PeaceAction : Action
    {
        public override void Execute(BaseStateMachine machine)
        {
            var navManager = machine.GetComponent<NavManager>();
            navManager.doPeacefulNav();
        }
    }
}
