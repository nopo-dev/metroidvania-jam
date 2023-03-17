using FSM.Abstracts;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Decisions/Random Attack")]
    public class RandomAttackDecision : Decision
    {
        [Range(0, Consts.NUM_SNAIL_SPECIAL_ATTACKS - 1)] // hack specific to snailman
        public int attackNumber;

        public override bool Decide(BaseStateMachine machine)
        {
            if (Utils.posMod(SnailMan.randomAttackDecider, Consts.NUM_SNAIL_SPECIAL_ATTACKS) == attackNumber)
            {
                machine.lockTransitions();
                return true;
            }
            return false;
        }
    }
}