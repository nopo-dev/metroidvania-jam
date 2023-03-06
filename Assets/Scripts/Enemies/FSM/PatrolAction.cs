using FSM.Abstracts;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Actions/Patrol")]
    public class PatrolAction : Action
    {
        public override void Execute(BaseStateMachine machine)
        {
            var patrolPoints = machine.GetComponent<PatrolPoints>(); // This seems hella inefficient
            var navManager = machine.GetComponent<NavManager>();
            
            if (navManager.hasReached())
            {
                navManager.setDestination(patrolPoints.getNext());
            }
        }
    }
}
