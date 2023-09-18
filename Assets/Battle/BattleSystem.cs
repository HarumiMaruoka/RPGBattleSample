// 日本語対応
using Actor;
using Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Battle
{
    public class BattleSystem
    {
        // 現在行動中のアクター
        private BattleActorModel _activeActor;
        //// 味方リスト
        private List<BattleActorModel> _allies = new List<BattleActorModel>();
        // 敵リスト
        private List<BattleActorModel> _enemies = new List<BattleActorModel>();
        // 戦闘に参加している全てのキャラ
        private List<BattleActorModel> _allBattleActor = new List<BattleActorModel>();
        // ユーザーが選んだスキル
        private BattleSkillModel _selectedSkill;
        // ユーザーが選んだターゲット
        private readonly List<BattleActorModel> _selectedTargets = new List<BattleActorModel>();

        public BattleActorModel ActiveActor => _activeActor;
        public IReadOnlyList<BattleActorModel> Allies => _allies;
        public IReadOnlyList<BattleActorModel> Enemies => _enemies;
        public IReadOnlyList<BattleActorModel> AllBattleActor => _allBattleActor;
        public BattleSkillModel SelectedSkill => _selectedSkill;
        public List<BattleActorModel> SelectedTargets => _selectedTargets;

        public bool IsVictory => _enemies.All(enemy => enemy.Myself.IsDead); // 全ての敵が死亡していたらtrueを返す。
        public bool IsDefeat => _allies.All(ally => ally.Myself.IsDead); // 全ての味方が死亡していたらtrueを返す。

        public void BattleStart()
        {
            // 全てのコレクションをクリアする。
            _allies.Clear();
            _enemies.Clear();
            _allBattleActor.Clear();

            // 味方を生成する。(ActorID 0番から3番を味方とする。)
            for (int i = 0; i < 4; i++)
            {
                var ally = GameDataBase.Instance.ActorDataBase.IDToActor[i];
                var allyForBattle = new BattleActorModel(ally);
                // 生成された味方をコレクションに登録する。
                _allies.Add(allyForBattle);
                _allBattleActor.Add(allyForBattle);
            }

            // ランダムに敵を抽選する。
            {
                var randomActor = GameDataBase.Instance.ActorDataBase.GetRandomActorModel();
                BattleActorModel enemy = new BattleActorModel(randomActor);

                // 抽選された敵をコレクションに格納する。
                _allBattleActor.Add(enemy);
                _enemies.Add(enemy);
            }

            foreach (var ally in _allies)
            {
                ally.Initialize(_allies, _enemies);
            }
            foreach (var enemy in _enemies)
            {
                enemy.Initialize(_enemies, _allies);
            }


            // ビューを作成する。
            foreach (var ally in _allies)
            {
                var view = GameObject.Instantiate(BattleResourceProvider.Current.BattleActorViewPrefab);
                view.transform.SetParent(BattleResourceProvider.Current.AllyParent);
                view.Initilize(ally);
            }
            foreach (var enemy in _enemies)
            {
                var view = GameObject.Instantiate(BattleResourceProvider.Current.BattleActorViewPrefab);
                view.transform.SetParent(BattleResourceProvider.Current.EnemyParent);
                view.Initilize(enemy);
            }
        }

        public void SelectActiveActor()
        {
            // 行動キャラの選定（ActionCountが最も低いActorの抽出。）
            _allBattleActor = _allBattleActor.OrderBy(a => a.Count).ToList();
            _activeActor = _allBattleActor.First();
        }
        public void UpdateActorCounters()
        {
            // 全キャラのカウントタイムの更新
            _allBattleActor = _allBattleActor.OrderBy(a => a.Count).ToList();
            var minCount = _allBattleActor.First().Count;
            foreach (var actor in _allBattleActor) actor.UpdateCountBy(-minCount);
        }

        public void SelectSkill(BattleSkillModel select) // ユーザーが選んだスキルをメンバ変数に保存する。
        {
            _selectedSkill = select;
        }

        public IReadOnlyList<BattleActorModel> GetSlectableTargets(BattleActorModel myself)
        {
            var targetingType = _selectedSkill.Skill.SelectableTargetType;
            var allies = myself.Allies;
            var enemies = myself.Enemies;


            List<BattleActorModel> result = new List<BattleActorModel>();

            if (targetingType.HasFlag(SelectableTargetType.Myself))
            { result.Add(ActiveActor); }
            if (targetingType.HasFlag(SelectableTargetType.Ally))
            { result.Remove(ActiveActor); result.AddRange(allies); }
            if (targetingType.HasFlag(SelectableTargetType.Enemy))
            { result.AddRange(enemies); }

            result.RemoveAll(actor =>
            // 選択対象が「生存しているアクター」であり、かつアクターが死亡している場合、アクターを除外する
            (targetingType.HasFlag(SelectableTargetType.Alive) && actor.IsDead) ||
            // 選択対象が「死亡しているアクター」であり、かつアクターが生きている場合、アクターを除外する
            (targetingType.HasFlag(SelectableTargetType.Death) && !actor.IsDead) ||
            // もし選択対象が「状態異常を持つアクター」であり、かつアクターが状態異常を持っていない場合、アクターを除外する
            (targetingType.HasFlag(SelectableTargetType.AbnormalStatus) && actor.Myself.StatusEffect.CurrentStatus != StatusEffectType.None));

            return result;
        }

        public static int CalculateInitialCount(int actorSpeed)
        {
            switch (actorSpeed)
            {
                // 参考: https://youtu.be/gynlMzrZVxI?t=112
                case 1: return UnityEngine.Random.Range(83, 85);
                case 2: return UnityEngine.Random.Range(77, 79);
                case 3: return UnityEngine.Random.Range(71, 73);
                case 4: return UnityEngine.Random.Range(59, 61);
                case 5: return UnityEngine.Random.Range(47, 49);
                case 6: return UnityEngine.Random.Range(46, 49);
                case 7: return UnityEngine.Random.Range(44, 46);
                case 8: return UnityEngine.Random.Range(43, 46);
                case 9: return UnityEngine.Random.Range(42, 46);
                case 10: return UnityEngine.Random.Range(41, 43);
            }

            throw new ArgumentException();
        }
    }
}
