using FSM.Abstracts;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Decisions/In Attack Range")]
    public class InAttackRangeDecision : Decision
    {
        public override bool Decide(BaseStateMachine machine)
        {
            var attacker = machine.GetComponent<Attacker>();
            return attacker.inRange();
        }
    }
}