// 日本語対応
using UnityEngine;

namespace Sky
{
    namespace Battle
    {
        public class Others : BattleStateBase
        {
            public override void Enter()
            {
                BattleDebugger.Current?.ClearText();
                BattleDebugger.Current?.AppendText("CurrentState is Others");
            }
            public override void Update()
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    var nextState = _stateMachine.States[BattleState.BattleStart];
                    _stateMachine.TransitionTo(nextState);
                }
            }
        }
    }
}