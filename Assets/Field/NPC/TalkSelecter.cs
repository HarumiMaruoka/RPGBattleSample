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

    public void CreateSelectables(IReadOnlyList<Glib.Talk.Node> nodes)
    {
        foreach (var node in nodes)
        {
            TalkSelectable selectable = null;
            selectable = GameObject.Instantiate(_talkSelectablePrefab, _selectableParent);
            selectable.Initialize(node);
            _alive.Add(selectable);
        }
    }
    public void DisposeSelectables()
    {
        foreach (var selectable in _alive)
        {
            selectable.Dispose();
            Destroy(selectable.gameObject);
        }
        _alive.Clear();
    }
}