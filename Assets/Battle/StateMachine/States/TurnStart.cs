// 日本語対応
using UnityEngine;

namespace Sky
{
    namespace Battle
    {
        public class TurnStart : BattleStateBase
        {
            public override void Enter()
            {
                BattleDebugger.Current?.ClearText();
                BattleDebugger.Current?.AppendText("CurrentState is TurnStart");
                // カウントを更新
                _battleSystem.UpdateActorCounters();
            }
            public override void Update()
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    var nextState = _stateMachine.States[BattleState.SelectActor];
                    _stateMachine.TransitionTo(nextState);
                }
            }
        }
    }
}