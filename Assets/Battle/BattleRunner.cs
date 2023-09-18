// 日本語対応
using UnityEngine;
using Battle;

public class BattleRunner : MonoBehaviour
{
    [SerializeField]
    private GameRunner _gameRunner;

    private readonly BattleSystem _battleSystem = new BattleSystem();
    private readonly BattleStateMachine _battleStateMachine = new BattleStateMachine();

    public BattleSystem BattleSystem => _battleSystem;
    public BattleStateMachine BattleStateMachine => _battleStateMachine;

    private void Start()
    {
        _battleStateMachine.Initialize(_gameRunner, this);
    }
    private void Update()
    {
        _battleStateMachine.Update();
    }
}
