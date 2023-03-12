using FSM.Abstracts;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Actions/Dying")]
    public class DyingAction : Action
    {
        protected void Start()
        {
            blockable = false;
        }

        public override void Execute(BaseStateMachine machine)
        {
            machine.lockState();
            var entity = machine.GetComponent<Enemy>();
            entity.playDyingAnimation(machine.unlockState);
        }
    }
}