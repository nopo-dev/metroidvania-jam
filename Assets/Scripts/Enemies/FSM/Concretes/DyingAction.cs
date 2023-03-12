using FSM.Abstracts;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Actions/Dying")]
    public class DyingAction : Action
    {
        public override void Execute(BaseStateMachine machine)
        {
            machine.lockState();
            var entity = machine.GetComponent<Enemy>();
            entity.playDyingAnimation(machine.unlockState);
        }
    }
}