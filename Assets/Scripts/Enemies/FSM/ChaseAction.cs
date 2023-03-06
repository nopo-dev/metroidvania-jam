using FSM.Abstracts;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Actions/Chase")]
    public class ChaseAction : Action
    {
        public override void Execute(BaseStateMachine machine)
        {
            var navManager = machine.GetComponent<NavManager>();
            var sightSensor = machine.GetComponent<SightSensor>();

            navManager.setDestination(sightSensor.getPlayerLoc());
        }
    }
}