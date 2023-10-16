// 日本語対応
using Sky.Battle;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using Sky.Actor;
using Sky.Battle.Skill;
using static Sky.Battle.Skill.Attack;

namespace Sky
{
    namespace Skill
    {
        public interface ISkillEffect
        {
            [Obsolete("現在新しいのを作成中。")]
            public UniTask Play(SkillBasicData usedSkill, BattleActorModel user, IReadOnlyList<BattleActorModel> targets, CancellationToken token);

            public SkillBasicData Info { get; }

            public int GetAttackCalculationResult(ActorModel actionActor, ActorModel targetActor)
            {
                return DamageCalculator.GetAttackCalculationResult(actionActor, targetActor);
            }

            public void ApplyAttackCalculationResult(ActorModel actionActor, ActorModel targetActor, int value, int count);

            public UniTask PlayAnimationAsync(Action<Dictionary<AttackResultRequestData, AttackResultResponseData>> playFunction, Func<bool> endTrigger);

            public void PlayAttackParticle(AttackParticle attackParticlePrefab, AttackParticle.InitializeData initializeData);
        }
    }
}