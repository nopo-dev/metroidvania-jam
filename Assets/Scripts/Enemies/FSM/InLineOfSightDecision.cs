using FSM.Abstracts;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Decisions/In Line Of Sight")]
    public class InLineOfSightDecision : Decision
    {
        public override bool Decide(BaseStateMachine machine)
        {
            var enemyInLineOfSight = machine.GetComponent<SightSensor>();
            return enemyInLineOfSight.ping();
        }
    }
}