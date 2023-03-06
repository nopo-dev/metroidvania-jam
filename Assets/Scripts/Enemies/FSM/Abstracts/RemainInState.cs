using UnityEngine;

namespace FSM.Abstracts
{
    [CreateAssetMenu(menuName = "FSM/Remain In State", fileName = "RemainInState")]
    public sealed class RemainInState : BaseState
    {
        public override void Execute(BaseStateMachine machine) { }
    }
}