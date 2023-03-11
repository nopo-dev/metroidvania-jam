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
            var attacker = machine.GetComponent<Attacker>();
            attacker.attack(machine.unlockState);
        }
    }
}