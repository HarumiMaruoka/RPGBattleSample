// 日本語対応
using System;
using System.Collections.Generic;

namespace Field
{
    namespace Talk
    {
        [Serializable]
        public class TalkData : ISelectable
        {
            public TalkData(string name, string text, List<TalkData> nexts)
            {
                _name = name;

                _text = text;
                _nexts = nexts;
            }

            private string _name;
            private string _text;
            private List<TalkData> _nexts = new List<TalkData>();

            private bool _isHovered = false;
            private bool _isSelected = false;

            public event Action OnHovered;
            public event Action OnUnhovered;
            public event Action OnSelected;
            public event Action OnDeselected;

            public string Text => _text;
            public IReadOnlyList<TalkData> Nexts => _nexts;

            public string Name => _name;

            public bool IsHovered => _isHovered;

            public bool IsSelected => _isSelected;

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