// 日本語対応
using System.Collections.Generic;
using Useful;

namespace Sky
{
    namespace Battle
    {
        public class BattleStateMachine : StateMachine
        {
            private readonly Dictionary<BattleState, BattleStateBase> _states = new Dictionary<BattleState, BattleStateBase>();
            public IReadOnlyDictionary<BattleState, BattleStateBase> States => _states;

            public void Initialize(GameRunner gameRunner, BattleRunner battleRunner)
            {
                // ステート用ディクショナリ初期化。
                _states.Add(BattleState.Others, new Others());
                _states.Add(BattleState.BattleStart, new BattleStart());
                _states.Add(BattleState.TurnStart, new TurnStart());
                _states.Add(BattleState.SelectActor, new SelectActor());
                _states.Add(BattleState.SelectCommand, new SelectCommand());
                _states.Add(BattleState.SelectTarget, new SelectTarget());
                _states.Add(BattleState.Action, new ActiveActorAction());
                _states.Add(BattleState.TurnEnd, new TurnEnd());
                _states.Add(BattleState.Win, new Win());
                _states.Add(BattleState.Lose, new Lose());

                // 各ステートの初期化。
                foreach (var state in _states)
                {
                    state.Value.Initialize(gameRunner, battleRunner.BattleSystem, battleRunner, this);
                }

                // 基底クラスの初期化処理も呼ぶ。
                base.Initialize(_states[BattleState.Others]);
            }
        }
        public enum BattleState  // 各ステートを表現する列挙体。 
        {
            Others,              // 戦闘外
            BattleStart,         // バトルスタート
            TurnStart,           // ターンスタート
            SelectActor,         // 行動アクターの抽出
            SelectCommand,       // コマンド選択
            SelectTarget,        // 効果対象者選択
            Action,              // アクション
            TurnEnd,             // ターンエンド
            Win,                 // ゲームクリア
            Lose,                // ゲームオーバー
        }
    }
}