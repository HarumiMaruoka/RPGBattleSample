// 日本語対応
using Skill;
using System;

namespace Battle
{
    public class BattleSkillModel : ISelectable
    {
        public BattleSkillModel(SkillModel skill)
        {
            _skill = skill;
        }

        public string Name => _skill.Name;

        private bool _isSelected = false;
        private bool _isHovered = false;
        private readonly SkillModel _skill;

        public event Action OnHovered;
        public event Action OnUnhovered;
        public event Action OnSelected;
        public event Action OnDeselected;

        public bool IsHovered => _isHovered;
        public bool IsSelected => _isSelected;
        public SkillModel Skill => _skill;

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