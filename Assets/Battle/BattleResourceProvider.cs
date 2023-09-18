// 日本語対応
using UnityEngine;

public class BattleResourceProvider : MonoBehaviour
{
    private static BattleResourceProvider _current = null;
    public static BattleResourceProvider Current => _current;

    [SerializeField]
    private BattleActorView _battleActorViewPrefab;
    [SerializeField]
    private Transform _allyParent;
    [SerializeField]
    private Transform _enemyParent;

    public BattleActorView BattleActorViewPrefab => _battleActorViewPrefab;

    public Transform AllyParent => _allyParent;

    public Transform EnemyParent => _enemyParent;

    private void OnEnable()
    {
        _current = this;
    }
    private void OnDisable()
    {
        _current = null;
    }
}
