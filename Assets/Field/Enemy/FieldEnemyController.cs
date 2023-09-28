// 日本語対応
using System.Collections.Generic;
using UnityEngine;

public class FieldEnemyController : MonoBehaviour
{
    [SerializeField]
    private List<int> _encounteredEnemyIDs;
    public IReadOnlyList<int> EncounteredEnemyIDs => _encounteredEnemyIDs;
}