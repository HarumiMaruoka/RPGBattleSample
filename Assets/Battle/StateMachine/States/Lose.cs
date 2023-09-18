// 日本語対応
using UnityEngine;

namespace Battle
{
    public class Lose : BattleStateBase
    {
        public override void Enter()
        {
            BattleDebugger.Current?.ClearText();
            BattleDebugger.Current?.AppendText("CurrentState is Lose");
        }
        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                var nextState = _stateMachine.States[BattleState.Others];
                _stateMachine.TransitionTo(nextState);
            }
        }
    }
}