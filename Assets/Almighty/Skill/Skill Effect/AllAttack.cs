// 日本語対応
using Sky.Battle;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using Sky.Actor;
using System;
using Sky.Battle.Skill;

namespace Sky
{
    namespace Skill
    {
        public class AllAttack : ISkillEffect
        {
            public async UniTask Play(SkillBasicData usedSkill, BattleActorModel user, IReadOnlyList<BattleActorModel> targets, CancellationToken token)
            {
                await UniTask.Delay(1000, cancellationToken: token); // 一秒待つ。
                UnityEngine.Debug.Log(user.Myself.Name + "による全体攻撃！");
                for (int i = 0; i < targets.Count; i++)
                {
                    //user.Myself.Attack(targets[i].Myself);
                    UnityEngine.Debug.Log("攻撃処理は未実装です。");
                }
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