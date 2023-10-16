// 日本語対応
using UnityEngine;

namespace Sky
{
    namespace Battle
    {
        public class BattleStart : BattleStateBase
        {
            public override void Enter()
            {
                BattleDebugger.Current?.ClearText();
                BattleDebugger.Current?.AppendText("CurrentState is BattleStart");

                _battleRunner.BattleSystem.BattleStart();
            }

            public override void Update()
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    var nextState = _stateMachine.States[BattleState.TurnStart];
                    _stateMachine.TransitionTo(nextState);
                }
            }
        }
    }
}