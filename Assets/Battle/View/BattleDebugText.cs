// 日本語対応
using Battle;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BattleDebugText : MonoBehaviour
{
    [SerializeField]
    private BattleRunner _battleRunner;
    [SerializeField]
    private DebugTextType _debugTextType;

    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
    }

    private void Update()
    {
        _text.text = GetDebugString();
    }

    private string GetDebugString()
    {
        switch (_debugTextType)
        {
            case DebugTextType.ActionActor:
                var activeActor = _battleRunner.BattleSystem.ActiveActor;
                if (activeActor != null && activeActor.Myself != null)
                {
                    return "Action Actor: " + activeActor.Myself.Name;
                }
                return "Action Actor: ";
            case DebugTextType.SelectedSkill:
                var selectedSkill = _battleRunner.BattleSystem.SelectedSkill;
                if (selectedSkill != null && selectedSkill.Skill != null)
                {
                    return "Selected Skill: " + selectedSkill.Skill.Name;
                }
                return "Selected Skill: ";
            case DebugTextType.SelectedTargets:
                var selectedTargets = _battleRunner.BattleSystem.SelectedTargets;
                if (selectedTargets != null && selectedTargets.Count != 0)
                {
                    return "Selected Targets: " + string.Join("\n", selectedTargets);
                }
                return "Selected Targets: ";
            default: return null;
        }
    }

    public enum DebugTextType
    {
        ActionActor,
        SelectedSkill,
        SelectedTargets,
    }
}