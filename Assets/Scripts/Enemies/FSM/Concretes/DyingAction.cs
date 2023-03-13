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
            var entity = machine.GetComponent<Enemy>();
            machine.lockState();
            entity.playDyingAnimation(machine.unlockTransitions);
        }
    }
}