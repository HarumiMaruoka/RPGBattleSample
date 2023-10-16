// 日本語対応
using Sky.Skill;
using System;

namespace Sky
{
    namespace Battle
    {
        public class BattleSkillModel : ISelectable
        {
            public BattleSkillModel(SkillBasicData skill)
            {
                _skillInfo = skill;
            }

            public string Name => _skillInfo.Name;

            private bool _isSelected = false;
            private bool _isHovered = false;
            private readonly SkillBasicData _skillInfo;

            public event Action OnHovered;
            public event Action OnUnhovered;
            public event Action OnSelected;
            public event Action OnDeselected;

            public bool IsHovered => _isHovered;
            public bool IsSelected => _isSelected;
            public SkillBasicData Skill => _skillInfo;

            public void OnHover()
            {
                _isHovered = true;
                OnHovered?.Invoke();
            }

            public void OnUnhover()
            {
                _isHovered = false;
                OnUnhovered?.Invoke();
            }
            public void OnSelect()
            {
                _isSelected = true;
                OnSelected?.Invoke();
            }

            public void OnDeselect()
            {
                _isSelected = false;
                OnDeselected?.Invoke();
            }
        }
    }
}