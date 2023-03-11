using FSM.Abstracts;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Actions/Attack")]
    public class AttackAction : Action
    {
        public override void Execute(BaseStateMachine machine)
        {
            var attacker = machine.GetComponent<Attacker>();
            if (attacker.isAttacking()) { return; }

            if (attacker.inRange()) // awk that we double-check it here
            {
                attacker.attack();
            }
        }
    }
}