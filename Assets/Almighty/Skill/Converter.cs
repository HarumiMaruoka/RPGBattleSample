// 日本語対応
using System;

namespace Skill
{
    public static class Converter
    {
        public static ISkillEffect ToSkillEffect(this int id)
        {
            switch (id)
            {
                case 1: return new SingleAttack();
                case 2: return new AllAttack();
                default: throw new ArgumentException("未定義のIDです。");
            }
        }

        public static ISkillEffect ToSkillEffect(this string str)
        {
            return int.Parse(str).ToSkillEffect();
        }

        public static SelectableTargetType ToSelectableTargetType(this int number)
        {
            return (SelectableTargetType)number;
        }

        public static SelectableTargetType ToSelectableTargetType(this string number)
        {
            return (SelectableTargetType)int.Parse(number);
        }

        public static TargetingType ToTargetingType(this string str)
        {
            return str switch
            {
                "SingleTarget" => TargetingType.SingleTarget,
                "AllTargets" => TargetingType.AllTargets,
                "AreaOfEffect" => TargetingType.AreaOfEffect,
                "MultipleOverlapping" => TargetingType.MultipleOverlapping,
                "MultipleUnique" => TargetingType.MultipleUnique,
                _ => throw new ArgumentException($"変換に失敗しました。 str: {str}"),
            };
        }

        public static SkillCategory ToSkillCategory(this string str)
        {
            return str switch
            {
                "Attack" => SkillCategory.Attack,
                "MagicAttack" => SkillCategory.MagicAttack,
                "Heal" => SkillCategory.Heal,
                "Buff" => SkillCategory.Buff,
                "Debuff" => SkillCategory.Debuff,
                _ => throw new ArgumentException($"変換に失敗しました。 str: {str}"),
            };
        }
    }
}