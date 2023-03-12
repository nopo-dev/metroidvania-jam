using FSM.Abstracts;
using UnityEngine;

/*
 * Written specifically for SnailMan.
 */
namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Decisions/Ready")]
    public class ReadyDecision : Decision
    {
        public override bool Decide(BaseStateMachine machine)
        {
            var snailman = machine.GetComponent<SnailMan>();
            if (snailman.readyToAttack())
            {
                snailman.deciding = false;
                return true;
            }
            return false;
        }
    }
}