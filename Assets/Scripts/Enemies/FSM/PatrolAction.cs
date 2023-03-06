using FSM.Abstracts;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Actions/Patrol")]
    public class PatrolAction : Action
    {
        public override void Execute(BaseStateMachine machine)
        {
            var patrolPoints = machine.GetComponent<PatrolPoints>();
            var navManager = machine.GetComponent<NavManager>();
            
            if (patrolPoints.hasReached(navManager))
            {
                navManager.setDestination(patrolPoints.getNext());
            }
        }
    }
}
