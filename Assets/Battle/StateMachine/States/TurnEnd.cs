// 日本語対応
using UnityEngine;

namespace Battle
{
    public class TurnEnd : BattleStateBase
    {
        public override void Enter()
        {
            BattleDebugger.Current?.ClearText();
            BattleDebugger.Current?.AppendText("CurrentState is TurnEnd");

            // 行動が終った人の素早さ、バフ、デバフ、使用スキルを基にカウントを再設定する。
            var usedSkill = _battleSystem.SelectedSkill;
            _battleSystem.ActiveActor.ActionCompleted(usedSkill);
        }
        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                var nextState = _stateMachine.States[BattleState.TurnStart];
                _stateMachine.TransitionTo(nextState);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                var nextState = _stateMachine.States[BattleState.Win];
                _stateMachine.TransitionTo(nextState);
            }
        }
    }
}