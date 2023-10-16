// 日本語対応
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Sky.Battle;
using Sky.Actor;
using System;
using Sky.Battle.Skill;

namespace Sky
{
    namespace Skill
    {
        public class SingleAttack : ISkillEffect
        {
            public async UniTask Play(SkillBasicData usedSkill, BattleActorModel user, IReadOnlyList<BattleActorModel> targets, CancellationToken token)
            {
                await UniTask.Delay(500, cancellationToken: token); // 0.5秒待つ。
                //user.Myself.Attack(targets[0].Myself);
                UnityEngine.Debug.Log("攻撃処理は未実装です。");
                await UniTask.Delay(500, cancellationToken: token); // 0.5秒待つ。
            }

            public SkillBasicData Info => throw new NotImplementedException();

            public void ApplyAttackCalculationResult(ActorModel actionActor, ActorModel targetActor, int value, int count)
            {
                throw new NotImplementedException();
            }

            public UniTask PlayAnimationAsync(Action<Dictionary<Attack.AttackResultRequestData, Attack.AttackResultResponseData>> playFunction, Func<bool> endTrigger)
            {
                throw new NotImplementedException();
            }

            public void PlayAttackParticle(AttackParticle attackParticlePrefab, AttackParticle.InitializeData initializeData)
            {
                throw new NotImplementedException();
            }
        }
    }
}