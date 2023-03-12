using FSM.Abstracts;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Decisions/No HP")]
    public class NoHPDecision : Decision
    {
        protected void Awake()
        {
            blockable = false;    
        }

        public override bool Decide(BaseStateMachine machine)
        {
            var entity = machine.GetComponent<Enemy>();
            return entity.noHP();
        }
    }
}