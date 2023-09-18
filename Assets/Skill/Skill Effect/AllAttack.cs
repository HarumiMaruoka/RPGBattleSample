// 日本語対応
using Battle;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace Skill
{
    public class AllAttack : ISkillEffect
    {
        public async UniTask Play(SkillModel usedSkill, BattleActorModel user, IReadOnlyList<BattleActorModel> targets, CancellationToken token)
        {
            await UniTask.Delay(1000, cancellationToken: token); // 一秒待つ。
            UnityEngine.Debug.Log(user.Myself.Name + "による全体攻撃！");
            for (int i = 0; i < targets.Count; i++)
            {
                user.Myself.Attack(targets[i].Myself);
            }
        }
    }
}