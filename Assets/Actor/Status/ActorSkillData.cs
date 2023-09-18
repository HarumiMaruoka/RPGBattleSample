// 日本語対応
using System.Collections.Generic;

namespace Actor
{
    public class ActorSkillData
    {
        private List<int> _usableSkillIDs = new List<int>() { 1, 2 }; // 使用可能スキル(ID)

        public IReadOnlyList<int> UsedSkillIDs => _usableSkillIDs;

        public ActorSkillData Clone()
        {
            var clone = new ActorSkillData();

            return clone;
        }
    }
}
