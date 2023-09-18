// 日本語対応
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class SelectCommand : BattleStateBase
    {
        private CommandSelecter<BattleSkillModel> _commandSelecter = new CommandSelecter<BattleSkillModel>();

        public CommandSelecter<BattleSkillModel> CommandSelecter => _commandSelecter;
        public override void Enter()
        {
            // 選択可能コマンドリストを作成。
            var selectable = new List<BattleSkillModel>();

            foreach (var skillID in _battleSystem.ActiveActor.Myself.SkillData.UsedSkillIDs)
            {
                selectable.Add(new BattleSkillModel(GameDataBase.Instance.SkillDataBase.IDToSkill[skillID]));
            }

            // 選択可能コマンドリストの割り当て。
            _commandSelecter.SetSelectableItems(selectable);

            // デバッグ用テキストを更新。
            UpdateDebugText();
        }
        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _commandSelecter.Next();
                UpdateDebugText();

            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _commandSelecter.Previous();
                UpdateDebugText();

            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                _battleSystem.SelectSkill(_commandSelecter.Hover);

                var nextState = _stateMachine.States[BattleState.SelectTarget];
                _stateMachine.TransitionTo(nextState);
            }
        }

        private void UpdateDebugText()
        {
            BattleDebugger.Current?.ClearText();
            BattleDebugger.Current?.AppendText("CurrentState is SelectCommand");
            BattleDebugger.Current?.AppendText($"\nCurrent: {_commandSelecter.Hover.Skill.Name}");
        }
    }
}