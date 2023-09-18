// 日本語対応
using UnityEngine;
using UnityEngine.UI;

public class BattleCommandView : MonoBehaviour
{
    [SerializeField]
    private Image _bgImage;
    [SerializeField]
    private Image _selectFrameImage;
    [SerializeField]
    private Text _nameText;

    [SerializeField]
    private Color _hoveredColor = Color.green;
    [SerializeField]
    private Color _unhoveredColor = Color.white;
    [SerializeField]
    private Color _selectedColor = Color.red;
    [SerializeField]
    private Color _deselectedColor = Color.clear;

    private ISelectable _selectable = null;

    public void Initialize(ISelectable selectable)
    {
        _selectable = selectable;
        _nameText.text = selectable.Name;

        _selectable.OnHovered += OnHovered;
        _selectable.OnUnhovered += OnUnhovered;
        _selectable.OnSelected += OnSelected;
        _selectable.OnDeselected += OnDeselected;
    }
    public void Dispose()
    {
        _selectable.OnHovered -= OnHovered;
        _selectable.OnUnhovered -= OnUnhovered;
        _selectable.OnSelected -= OnSelected;
        _selectable.OnDeselected -= OnDeselected;
    }

    public void OnHovered()
    {
        _bgImage.color = _hoveredColor;
    }
    public void OnUnhovered()
    {
        _bgImage.color = _unhoveredColor;
    }
    public void OnSelected()
    {
        _selectFrameImage.color = _selectedColor;
    }
    public void OnDeselected()
    {
        _selectFrameImage.color = _deselectedColor;
    }
}