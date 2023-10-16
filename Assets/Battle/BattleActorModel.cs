// 日本語対応
using Sky.Actor;
using System;
using System.Collections.Generic;

namespace Sky
{
    namespace Battle
    {
        public class BattleActorModel : ISelectable
        {
            public BattleActorModel(ActorModel myself)
            {
                var count = BattleSystem.CalculateInitialCount(myself.TotalAttributeStatus.Speed);

                _myself = myself; _count = count;
            }
            public void Initialize(IReadOnlyList<BattleActorModel> allies, IReadOnlyList<BattleActorModel> enemies)
            {
                _allies = allies; _enemies = enemies;
            }

            private IReadOnlyList<BattleActorModel> _allies;
            private IReadOnlyList<BattleActorModel> _enemies;

            private ActorModel _myself;
            private int _count;

            public string Name => _myself.Name;

            public event Action OnHovered;
            public event Action OnUnhovered;
            public event Action OnSelected;
            public event Action OnDeselected;

            public IReadOnlyList<BattleActorModel> Allies => _allies;
            public IReadOnlyList<BattleActorModel> Enemies => _enemies;
            public ActorModel Myself => _myself;
            public int Count => _count;
            public bool IsDead => _myself.IsDead;

            private bool _isHovered = false;
            private bool _isSelected = false;

            public bool IsHovered => _isHovered;
            public bool IsSelected => _isSelected;

            public void UpdateCountBy(int value)
            {
                _count += value;
            }

            public override string ToString()
            {
                return _myself.ToString() + $"Count: {_count}";
            }

            public void ActionCompleted(BattleSkillModel usedSkill) // 行動終了時に実行される。
            {
                var speed = _myself.TotalAttributeStatus.Speed;
                var weight = usedSkill.Skill.Weight;
                var statusEffect = _myself.StatusEffect.CurrentStatus;
                _count = CalculateTotalWeight(speed, weight, statusEffect);
            }

            public void OnHover()
            {
                _isHovered = true;
                OnHovered?.Invoke();
            }

            public void OnUnhover()
            {
                _isHovered = false;
                OnUnhovered?.Invoke();
            }

            public void OnSelect()
            {
                _isSelected = true;
                OnSelected?.Invoke();
            }

            public void OnDeselect()
            {
                _isSelected = false;
                OnDeselected?.Invoke();
            }

            private int CalculateTotalWeight(int actorSpeed, int skillWeight, StatusEffectType statusEffect)
            {
                // 素早さの閾値とそれに対応する負荷値を定義
                int[] speedThresholds = { 0, 6, 9, 11, 14, 16, 18, 22, 28, 34, 43, 61, 97, 169 };
                int[] weightValues = { 18, 16, 14, 10, 8, 7, 6, 5, 4, 3 };

                // actorSpeed を閾値と比較し、対応する負荷値のインデックスを見つける
                int actorSpeedIndex = Array.FindIndex(speedThresholds, threshold => actorSpeed < threshold);

                // インデックスが無効な場合は例外をスロー
                if (actorSpeedIndex == -1)
                {
                    throw new ArgumentException("Invalid actorSpeed");
                }

                // 総負荷を計算し、スキルの重みを乗算
                int totalWeight = weightValues[actorSpeedIndex] * skillWeight;

                // ヘイストとスロウのステータス効果を考慮
                if (statusEffect.HasFlag(StatusEffectType.Haste))
                {
                    totalWeight /= 2; // ヘイスト状態なら負荷を半分にする
                }
                else if (statusEffect.HasFlag(StatusEffectType.Slow))
                {
                    totalWeight *= 2; // スロウ状態なら負荷を2倍にする
                }

                return totalWeight;
            }
        }
    }
}