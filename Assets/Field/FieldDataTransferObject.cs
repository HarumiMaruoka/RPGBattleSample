// 日本語対応
using UnityEngine;
using UnityEngine.UI;

public class FieldDataTransferObject : MonoBehaviour
{
    private static FieldDataTransferObject _current;
    public static FieldDataTransferObject Current => _current;

    [SerializeField]
    private Text _talkText;

    private void OnEnable()
    {
        _current = this;
    }
    private void OnDisable()
    {
        _current = null;
    }
}