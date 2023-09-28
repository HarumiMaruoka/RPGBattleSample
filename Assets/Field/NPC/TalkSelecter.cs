// 日本語対応
using System.Collections.Generic;
using UnityEngine;

public class TalkSelecter : MonoBehaviour
{
    [SerializeField]
    private Transform _selectableParent;
    [SerializeField]
    private TalkSelectable _talkSelectablePrefab;

    private readonly List<TalkSelectable> _alive = new List<TalkSelectable>();
    private readonly Stack<TalkSelectable> _disposed = new Stack<TalkSelectable>();

    public void CreateSelectables(IReadOnlyList<Glib.Talk.Node> nodes)
    {
        foreach (var node in nodes)
        {
            TalkSelectable selectable = null;
            if (_disposed.Count == 0)
            {
                selectable = GameObject.Instantiate(_talkSelectablePrefab, _selectableParent);
            }
            else
            {
                selectable = _disposed.Pop();
                selectable.gameObject.SetActive(true);
            }
            selectable.Initialize(node);
            _alive.Add(selectable);
        }
    }
    public void DisposeSelectables()
    {
        foreach (var selectable in _alive)
        {
            _disposed.Push(selectable);
            selectable.gameObject.SetActive(false);
        }
        _alive.Clear();
    }
}