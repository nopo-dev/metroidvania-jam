using FSM.Abstracts;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Actions/Peace")]
    public class PeaceAction : Action
    {
        public override void Execute(BaseStateMachine machine)
        {
            if (machine.actionLocked) { return; }
            var entity = machine.GetComponent<Enemy>();
            entity.doPeacefulNav();
        }
    }
}
