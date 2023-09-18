// 日本語対応
using System;
using System.Collections.Generic;

public class CommandSelecter<T> where T : ISelectable
{
    private int _index = 0;
    private IReadOnlyList<T> _selectables = new List<T>();
    private readonly List<T> _selecteds = new List<T>();

    public IReadOnlyList<T> Selectables => _selectables;
    public IReadOnlyList<T> Selecteds => _selecteds;
    public T Hover => _selectables[_index];

    // 使い始めるたびに呼び出す。
    public void SetSelectableItems(IReadOnlyList<T> selectables)
    {
        _index = 0;
        _selecteds.Clear();
        _selectables = selectables;
        Hover.OnHover();
    }

    public void Select()
    {
        _selectables[_index].OnSelect();
        _selecteds.Add(_selectables[_index]);
    }
    public void Deselect()
    {
        _selectables[_index].OnDeselect();
        _selecteds.Remove(_selectables[_index]);
    }
    public bool IsSelected(T item)
    {
        return _selecteds.Contains(item);
    }
    public bool IsSelected()
    {
        var currentHovered = _selecteds[_index];
        return _selecteds.Contains(currentHovered);
    }
    public void Next()
    {
        _selectables[_index].OnUnhover();
        _index++;
        if (_index == _selectables.Count) _index = 0;
        _selectables[_index].OnHover();
    }
    public void Previous()
    {
        _selectables[_index].OnUnhover();
        _index--;
        if (_index == -1) _index = _selectables.Count - 1;
        _selectables[_index].OnHover();
    }
}
public interface ISelectable
{
    public string Name { get; }

    public bool IsHovered { get; }
    public bool IsSelected { get; }

    public void OnHover();
    public void OnUnhover();
    public void OnSelect();
    public void OnDeselect();

    public event Action OnHovered;
    public event Action OnUnhovered;
    public event Action OnSelected;
    public event Action OnDeselected;
}