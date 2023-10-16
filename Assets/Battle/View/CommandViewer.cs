// 日本語対応
using System.Collections.Generic;
using UnityEngine;
using Sky.Battle;

public class CommandViewer : MonoBehaviour
{
    [SerializeField]
    private BattleRunner _battleRunner;
    [SerializeField]
    private Transform _viewParent;
    [SerializeField]
    private BattleCommandView _battleCommandViewPrefab;

    private List<BattleCommandView> _views = new List<BattleCommandView>();
    private IReadOnlyList<ISelectable> _selectables = null;

    private void OnEnable()
    {
        _battleRunner.BattleStateMachine.OnStateChanged += OnStateChanged;
    }
    private void OnDisable()
    {
        _battleRunner.BattleStateMachine.OnStateChanged -= OnStateChanged;
    }

    private void OnSelectablesChanged(IReadOnlyList<ISelectable> newList)
    {
        foreach (var view in _views)
        {
            view.Dispose();
            GameObject.Destroy(view.gameObject);
        }
        _views.Clear();

        if (newList == null) return;

        foreach (var selectable in newList)
        {
            var view = GameObject.Instantiate(_battleCommandViewPrefab, _viewParent);
            view.Initialize(selectable);
            _views.Add(view);

            // 最初の状態を適用。（最初からホバー状態ならOnHovered()を、そうでなければOnDeselected()を実行する。）
            if (selectable.IsHovered)
                view.OnHovered();
            else
                view.OnDeselected();

            // 最初の状態を適用。（最初から選択状態ならOnSelected()を、そうでなければOnDeselected()を実行する。）
            if (selectable.IsSelected)
                view.OnSelected();
            else
                view.OnDeselected();
        }
    }

    private void OnStateChanged(State changedState)
    {
        IReadOnlyList<ISelectable> oldList = null;
        _selectables = null;

        var selectCommand = changedState as SelectCommand;
        if (selectCommand != null)
        {
            oldList = _selectables;
            _selectables = selectCommand.CommandSelecter.Selectables;
        }

        var selectTarget = changedState as SelectTarget;
        if (selectTarget != null)
        {
            oldList = _selectables;
            _selectables = selectTarget.TargetSelector.Selectables;
        }

        OnSelectablesChanged(_selectables);
    }
}