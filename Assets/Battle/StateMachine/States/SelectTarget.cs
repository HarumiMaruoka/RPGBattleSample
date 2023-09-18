// 日本語対応
using Skill;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class SelectTarget : BattleStateBase
    {
        private readonly TargetSelector _targetSelecter = new TargetSelector();

        public TargetSelector TargetSelector => _targetSelecter;

        public override void Enter()
        {
            // 選択状態をクリア。
            _battleSystem.SelectedTargets.Clear();

            // ターゲット選択用クラスの初期化。
            var activeActor = _battleSystem.ActiveActor;
            var selectables = _battleSystem.GetSlectableTargets(activeActor);
            // 選択可能な対象が1人もいなければSkillSelectに戻る。
            if (selectables.Count == 0)
            {
                Back();
                return;
            }

            // ターゲット選択用クラスの初期化。
            _targetSelecter.Initialize(_battleSystem.SelectedSkill, ref selectables);

            // アクションの登録。
            _targetSelecter.Decisioned = Step;
            _targetSelecter.Cancel = Back;

            UpdateDebugText();
        }
        public override void Update()
        {
            UpdateDebugText();
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _targetSelecter.Next();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _targetSelecter.Previous();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _targetSelecter.Deselect();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _targetSelecter.Select();
            }
        }

        private void Back()
        {
            var previousState = _stateMachine.States[BattleState.SelectCommand];
            _stateMachine.TransitionTo(previousState);
        }

        private void Step()
        {
            _battleSystem.SelectedTargets.AddRange(_targetSelecter.Selecteds);

            var nextState = _stateMachine.States[BattleState.Action];
            _stateMachine.TransitionTo(nextState);
        }

        private void UpdateDebugText()
        {
            BattleDebugger.Current?.ClearText();

            BattleDebugger.Current?.AppendText("CurrentState is SelectTarget");
            // ホバー中のターゲットと選択済みのターゲットを表示する。
            var hovereds = _targetSelecter.Hovereds;
            for (int i = 0; i < hovereds.Count; i++)
            {
                BattleDebugger.Current?.AppendText($"\nHovered: {hovereds[i].Myself.Name}");
            }
            var selecteds = _targetSelecter.Selecteds;
            for (int i = 0; i < selecteds.Count; i++)
            {
                BattleDebugger.Current?.AppendText($"\nSelected: {selecteds[i].Myself.Name}");
            }
        }
    }
    public class TargetSelector
    {
        private int _index = 0;
        private BattleSkillModel _useSkill = null; // 使用スキル
        private readonly List<BattleActorModel> _hovereds = new List<BattleActorModel>();  // ホバー中
        private IReadOnlyList<BattleActorModel> _selectables = null;                       // 選択肢
        private readonly List<BattleActorModel> _selecteds = new List<BattleActorModel>(); // 選択済み

        public Action Cancel;     // _selected.Count==0のときかつ、Deselect()が呼ばれたとき発火する。
        public Action Decisioned; // 選択状態が満たされたとき発火する。
        public IReadOnlyList<BattleActorModel> Hovereds => _hovereds;
        public IReadOnlyList<BattleActorModel> Selectables => _selectables;
        public IReadOnlyList<BattleActorModel> Selecteds => _selecteds;

        public void Initialize(BattleSkillModel useSkill, ref IReadOnlyList<BattleActorModel> selectables)
        {
            _index = 0;
            _hovereds.Clear();
            _selecteds.Clear();
            _useSkill = useSkill;

            // 古いコレクションの状態をクリア。
            if (_selectables != null && _selectables.Count != 0)
            {
                foreach (var item in _selectables) { item.OnUnhover(); item.OnDeselect(); }
            }

            _selectables = selectables;

            var targetingType = _useSkill.Skill.TargetingType;
            var targetingNumber = _useSkill.Skill.TargetNumber;
            switch (targetingType)
            {
                case TargetingType.SingleTarget:
                    _hovereds.Add(_selectables[_index]);
                    break;
                case TargetingType.AllTargets:
                    _hovereds.AddRange(Selectables);
                    break;
                case TargetingType.AreaOfEffect:
                    targetingNumber /= 2;
                    for (int i = -targetingNumber; i <= targetingNumber; i++)
                    {
                        if (IsInIndex(i + _index))
                        {
                            _hovereds.Add(_selectables[i + _index]);
                            _selectables[i + _index].OnHover();
                        }
                    }
                    break;
                case TargetingType.MultipleOverlapping:
                    _hovereds.Add(_selectables[_index]);
                    break;
                case TargetingType.MultipleUnique:
                    _hovereds.Add(_selectables[_index]);
                    break;
            }
            foreach (var hover in _hovereds)
                hover.OnHover();
        }

        public void Next()
        {
            var oldIndex = _index;
            _index++;
            if (_index == _selectables.Count) _index = 0;
            var newIndex = _index;

            OnIndexChanged(oldIndex, newIndex);
        }

        public void Previous()
        {
            var oldIndex = _index;
            _index--;
            if (_index == -1) _index = _selectables.Count - 1;
            var newIndex = _index;

            OnIndexChanged(oldIndex, newIndex);
        }

        public void Select()
        {
            // ホバー中のターゲットを選択済みに追加。
            _selecteds.AddRange(_hovereds);

            var targetingType = _useSkill.Skill.TargetingType;
            var targetNumber = _useSkill.Skill.TargetNumber;

            // 単一、全体、範囲は一回の選択で決定する。
            if (targetingType == TargetingType.SingleTarget ||
                targetingType == TargetingType.AllTargets ||
                targetingType == TargetingType.AreaOfEffect)
            {
                Decisioned?.Invoke();
                return;
            }
            // 複数選択は回数が満たされた時に決定する。
            if ((targetingType == TargetingType.MultipleUnique ||
                targetingType == TargetingType.MultipleOverlapping) &&
                _selecteds.Count == targetNumber)
            {
                Decisioned?.Invoke();
                return;
            }
            // 重複不可の場合、次のターゲットをホバーする。
            if (targetingType == TargetingType.MultipleUnique)
            {
                Next();
            }
        }

        public void Deselect()
        {
            if (_selecteds.Count == 0)
            {
                Cancel?.Invoke();
                return;
            }

            var lastSelected = _selecteds[_selectables.Count - 1];
            _selecteds.RemoveAt(_selectables.Count - 1);
            lastSelected.OnDeselect();
        }

        private void OnIndexChanged(int oldIndex, int newIndex)
        {
            var targetNumber = _useSkill.Skill.TargetNumber;
            var targetingType = _useSkill.Skill.TargetingType;
            var oldHoverItem = _selectables[oldIndex];
            var newHoverItem = _selectables[newIndex];

            switch (targetingType)
            {
                case TargetingType.SingleTarget:
                    _hovereds.Remove(oldHoverItem);
                    oldHoverItem.OnUnhover();
                    _hovereds.Add(newHoverItem);
                    newHoverItem.OnHover();
                    break;
                case TargetingType.AllTargets:
                    // ずっと全選択状態なのでインデックスが変わってもすることがない。
                    break;
                case TargetingType.AreaOfEffect:
                    _hovereds.Clear();
                    targetNumber /= 2;
                    for (int i = -targetNumber; i <= targetNumber; i++)
                    {
                        if (IsInIndex(i + newIndex))
                        {
                            _hovereds.Add(_selectables[i + newIndex]);
                            _selectables[i + newIndex].OnHover();
                        }
                    }
                    break;
                case TargetingType.MultipleOverlapping: // 基本的にはSingleと同じ挙動
                    _hovereds.Remove(oldHoverItem);
                    oldHoverItem.OnUnhover();
                    _hovereds.Add(newHoverItem);
                    newHoverItem.OnHover();
                    break;
                case TargetingType.MultipleUnique:

                    _hovereds.Remove(oldHoverItem);
                    oldHoverItem.OnUnhover();

                    // _selectable[newIndex]が選択済みならもう一個奥へ探索する。
                    // 30回くらい探索したらエラーを投げる。
                    int delta = oldIndex < newIndex ? 1 : -1; // インデックスが増加していたら1。減っていたら-1。

                    for (int i = delta; i < 30 * delta; i += delta)
                    {
                        if (!IsSelected(i + newIndex))
                        {
                            _hovereds.Add(_selectables[i + newIndex]);
                            _selectables[i + newIndex].OnHover();
                            break;
                        }
                    }
                    throw new ArgumentException();
            }
        }

        public bool IsInIndex(int index)
        {
            return index >= 0 && index < _selectables.Count;
        }

        public bool IsSelected(int index)
        {
            return _selecteds.Contains(_selectables[index]);
        }
    }
}