// 日本語対応

namespace Battle
{
    public class BattleStateBase : State
    {
        protected GameRunner _gameRunner = null;
        protected BattleRunner _battleRunner = null;
        protected BattleSystem _battleSystem = null;
        protected BattleStateMachine _stateMachine = null;

        public void Initialize(GameRunner gameRunner, BattleSystem battleSystem, BattleRunner battleRunner, BattleStateMachine stateMachine)
        {
            _gameRunner = gameRunner; _battleSystem = battleSystem; _battleRunner = battleRunner; _stateMachine = stateMachine;
        }
    }
}