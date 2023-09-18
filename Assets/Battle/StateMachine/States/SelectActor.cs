// 日本語対応
using UnityEngine;

namespace Battle
{
    public class SelectActor : BattleStateBase
    {
        public override void Enter()
        {
            BattleDebugger.Current?.ClearText();
            BattleDebugger.Current?.AppendText("CurrentState is SelectActor");
            // 行動アクターを抽出。
            _battleRunner.BattleSystem.SelectActiveActor();
            BattleDebugger.Current?.AppendText($"\nActive Actor: {_battleSystem.ActiveActor}");
        }
        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                var nextState = _stateMachine.States[BattleState.SelectCommand];
                _stateMachine.TransitionTo(nextState);
            }
        }
    }
}