using FSM.Abstracts;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Actions/Attack")]
    public class AttackAction : Action
    {
        public override void Execute(BaseStateMachine machine)
        {
            machine.lockState();
            var entity = machine.GetComponent<Enemy>();
            entity.attack(machine.unlockState);
        }
    }
}