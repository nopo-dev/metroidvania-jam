using FSM.Abstracts;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Actions/Special Attack")]
    public class SpecialAttackAction : Action
    {
        [SerializeField] SnailMan.SnailAttack attack;
        public override void Execute(BaseStateMachine machine)
        {
            if (machine.actionLocked) { return; }
            var entity = machine.GetComponent<SnailMan>();
            machine.lockState();
            entity.specialAttack(attack, machine.unlockTransitions);
        }
    }
}