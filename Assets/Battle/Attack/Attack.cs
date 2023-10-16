// 日本語対応
using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Sky.Actor;
using Sky.Skill;

namespace Sky
{
    namespace Battle
    {
        namespace Skill
        {
            public class Attack : ISkillEffect
            {

                public UniTask Play(SkillBasicData usedSkill, BattleActorModel user, IReadOnlyList<BattleActorModel> targets, CancellationToken token)
                {
                    throw new NotImplementedException();
                }

                public Attack(SkillBasicData info)
                {
                    _info = info;
                }

                private SkillBasicData _info;

                public SkillBasicData Info => _info;

                public int GetAttackCalculationResult(ActorModel actionActor, ActorModel targetActor)
                {
                    return DamageCalculator.GetAttackCalculationResult(actionActor, targetActor);
                }

                public void ApplyAttackCalculationResult(ActorModel actionActor, ActorModel targetActor, int value, int count)
                {
                    targetActor.Damage(value);
                    var requestData = new AttackResultRequestData(actionActor, targetActor, count);
                    var responseData = new AttackResultResponseData(value);
                    _attackResultCollection.Add(requestData, responseData);
                }

                public async UniTask PlayAnimationAsync(Action<Dictionary<AttackResultRequestData, AttackResultResponseData>> playFunction, Func<bool> endTrigger)
                {
                    playFunction?.Invoke(this._attackResultCollection);
                    await UniTask.WaitUntil(endTrigger);
                }

                public void PlayAttackParticle(AttackParticle attackParticlePrefab, AttackParticle.InitializeData initializeData)
                {
                    // 適切なポジションに、
                    // 攻撃時のパーティクルを生成する処理を記述する。
                    throw new NotImplementedException();
                }

                private Dictionary<AttackResultRequestData, AttackResultResponseData> _attackResultCollection = new();

                public IReadOnlyDictionary<AttackResultRequestData, AttackResultResponseData> AttackResultCollection => _attackResultCollection;

                public struct AttackResultRequestData // 攻撃計算結果取得用のキーデータ
                {
                    public AttackResultRequestData(ActorModel actionActor, ActorModel targetActor, int count)
                    {
                        _actionActor = actionActor;
                        _targetActor = targetActor;
                        _count = count;
                    }

                    private ActorModel _actionActor; // 誰から
                    private ActorModel _targetActor; // 誰へ
                    private int _count; // 何回目の攻撃か

                    public ActorModel ActionActor { get => _actionActor; set => _actionActor = value; }
                    public ActorModel TargetActor { get => _targetActor; set => _targetActor = value; }
                    public int Count { get => _count; set => _count = value; }
                }

                public struct AttackResultResponseData // 攻撃計算結果取得用のバリューデータ
                {
                    public AttackResultResponseData(int value) { _value = value; }

                    private int _value; // 何ダメージ与えたか

                    public int Value { get => _value; set => _value = value; }
                }

                public void ClearAttackResultCollection()
                {
                    _attackResultCollection.Clear();
                }
            }
        }
    }
}