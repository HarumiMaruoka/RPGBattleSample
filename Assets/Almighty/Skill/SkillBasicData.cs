// 日本語対応
using System;

namespace Sky
{
    namespace Skill
    {
        public class SkillBasicData
        {
            public SkillBasicData(int id, string name, string description,
                SkillCategory category, int weight, SelectableTargetType selectableTargetType,
                int targetNum, TargetingType targetingType, ISkillEffect effect)
            {
                _id = id;
                _name = name;
                _description = description;
                _category = category;
                _weight = weight;
                _selectableTargetType = selectableTargetType;
                _targetNum = targetNum;
                _targetingType = targetingType;
                _effect = effect;
            }
            public static SkillBasicData CreateSkill(string[] strs)
            {
                var id = int.Parse(strs[0]);
                var name = strs[1];
                var description = strs[2];
                var category = strs[3].ToSkillCategory();
                var weight = int.Parse(strs[4]);
                var selectableTargetType = strs[5].ToSelectableTargetType();
                var targetingType = strs[6].ToTargetingType();
                var targetNum = int.Parse(strs[7]);
                var skillEffect = id.ToSkillEffect();

                return new SkillBasicData(id, name, description, category, weight, selectableTargetType, targetNum, targetingType, skillEffect);
            }

            private readonly int _id;                 // ID
            private readonly string _name;            // 名前
            private readonly string _description;     // 説明文
            private readonly SkillCategory _category; // 種類
            private readonly int _weight;             // スキルの使用負荷

            private readonly SelectableTargetType _selectableTargetType; // だれを選択できるか
            private readonly int _targetNum;                             // 何人選択できるか
            private readonly TargetingType _targetingType;               // どのように選択するか

            private readonly ISkillEffect _effect; // スキルの効果

            public int Id => _id;
            public string Name => _name;
            public string Description => _description;
            public SkillCategory Category => _category;
            public int Weight => _weight;
            public SelectableTargetType SelectableTargetType => _selectableTargetType;
            public int TargetNumber => _targetNum;
            public TargetingType TargetingType => _targetingType;
            public ISkillEffect Effect => _effect;
        }

        public enum SkillCategory // スキルのカテゴリ
        {
            Attack,
            MagicAttack,
            Heal,
            Buff,
            Debuff,
        }
        /// <summary>
        /// 誰を選択できるか（選択可能な対象の種類）
        /// </summary>
        [Flags]
        public enum SelectableTargetType
        {
            // 自分
            Myself = 1,
            // 味方
            Ally = 2,
            // 敵
            Enemy = 4,
            // 生きているキャラ
            Alive = 8,
            // 死亡しているキャラ
            Death = 16,
            // 状態異常のキャラ
            AbnormalStatus = 32,
        }
        /// <summary>
        /// どのように選択するか（対象の選択方法の種類）
        /// </summary>
        public enum TargetingType
        {
            /// <summary> 単一ターゲット </summary>
            SingleTarget,
            /// <summary> 全体ターゲット </summary>
            AllTargets,
            /// <summary> 範囲ターゲット </summary>
            AreaOfEffect,
            /// <summary> 複数ターゲット（重複化） </summary>
            MultipleOverlapping,
            /// <summary> 複数ターゲット（重複不可） </summary>
            MultipleUnique,

            // 以下は自動系。敵や混乱時に使用する。
            /// <summary> ランダム </summary>
            Random,
        }
    }
}