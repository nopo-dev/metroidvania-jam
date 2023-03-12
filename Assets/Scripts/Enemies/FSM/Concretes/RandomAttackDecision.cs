using FSM.Abstracts;
using UnityEngine;

namespace FSM
{
    [CreateAssetMenu(menuName = "FSM/Decisions/Random Attack")]
    public class RandomAttackDecision : Decision
    {
        [Range(0.0f, 1.0f)]
        public float conditionalChance = 0.5f;
        private bool _firedOnce;
        private System.Random _rand = new System.Random();

        public override bool Decide(BaseStateMachine machine)
        {
            if (!_firedOnce)
            {
                _firedOnce = true;
                return true;
            }
            return _rand.NextDouble() < conditionalChance;
        }
    }
}