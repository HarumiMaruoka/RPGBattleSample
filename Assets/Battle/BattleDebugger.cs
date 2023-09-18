// 日本語対応
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class BattleDebugger : MonoBehaviour
{
    private static BattleDebugger _current = null;
    public static BattleDebugger Current => _current;

    [SerializeField]
    private Text _debugText; // デバッグ用テキスト

    private readonly StringBuilder _stringBuilder = new StringBuilder();

    public void OnEnable()
    {
        _current = this;
    }
    private void OnDisable()
    {
        _current = null;
    }

    public void AppendText(string text)
    {
        _stringBuilder.Append(text);
        _debugText.text = _stringBuilder.ToString();
    }
    public void ClearText()
    {
        _stringBuilder.Clear();
        _debugText.text = null;
    }
}
