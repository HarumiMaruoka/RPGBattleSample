// 日本語対応
using Battle;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace Skill
{
    public class SingleAttack : ISkillEffect
    {
        public async UniTask Play(SkillModel usedSkill, BattleActorModel user, IReadOnlyList<BattleActorModel> targets, CancellationToken token)
        {
            await UniTask.Delay(500, cancellationToken: token); // 0.5秒待つ。
            user.Myself.Attack(targets[0].Myself);
            await UniTask.Delay(500, cancellationToken: token); // 0.5秒待つ。
        }
    }
}