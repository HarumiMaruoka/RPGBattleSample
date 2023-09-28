// 日本語対応
using UnityEngine;
using System;

public class PlayerStateController : MonoBehaviour
{
    private FieldPlayerState _currentState;
    public FieldPlayerState CurrentState => _currentState;

    public void AddState(FieldPlayerState add)
    {
        _currentState |= add;
    }
    public void RemoveState(FieldPlayerState remove)
    {
        _currentState &= ~remove;
    }
    public bool HasState(FieldPlayerState state)
    {
        return _currentState.HasFlag(state);
    }
}

[Serializable]
public enum FieldPlayerState : int
{
    None = 0,
    Everything = -1,
    Run = 1,
    Talk = 2,
    Rise = 4,
    Fall = 8,
}