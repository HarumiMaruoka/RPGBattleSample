// 日本語対応
using Battle;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace Skill
{
    public interface ISkillEffect
    {
        public UniTask Play(SkillModel usedSkill, BattleActorModel user, IReadOnlyList<BattleActorModel> targets, CancellationToken token);
    }
}