using FSM.Abstracts;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Decisions/No HP")]
    public class NoHPDecision : Decision
    {
        // TODO: noHP cannot be state-locked.
        public override bool Decide(BaseStateMachine machine)
        {
            var entity = machine.GetComponent<Enemy>();
            return entity.noHP();
        }
    }
}