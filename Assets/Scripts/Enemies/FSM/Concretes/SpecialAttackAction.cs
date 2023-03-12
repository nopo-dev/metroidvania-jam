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
            if (machine.locked) { return; }
            machine.lockState();
            var entity = machine.GetComponent<SnailMan>();
            entity.specialAttack(attack, machine.unlockState);
        }
    }
}