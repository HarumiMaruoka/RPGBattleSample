// 日本語対応
using Cysharp.Threading.Tasks;

namespace Battle
{
    public class ActiveActorAction : BattleStateBase
    {
        public override async void Enter()
        {
            BattleDebugger.Current?.ClearText();
            BattleDebugger.Current?.AppendText("CurrentState is Action");

            // ActiveActorが持つ SelectedSkillの効果を SelectedTargetsに対して適用する。

            var actor = _battleSystem.ActiveActor;
            var skill = _battleSystem.SelectedSkill.Skill;
            var targets = _battleSystem.SelectedTargets;

            await skill.Effect.Play(skill, actor, targets, _gameRunner.GetCancellationTokenOnDestroy());

            Transition();
        }

        private void Transition()
        {
            BattleStateBase nextState = null;
            if (_battleSystem.IsVictory)
            {
                nextState = _stateMachine.States[BattleState.Win];
            }
            else if (_battleSystem.IsDefeat)
            {
                nextState = _stateMachine.States[BattleState.Lose];
            }
            else
            {
                nextState = _stateMachine.States[BattleState.TurnEnd];
            }
            _stateMachine.TransitionTo(nextState);
        }
    }
}