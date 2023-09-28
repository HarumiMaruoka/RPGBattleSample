// 日本語対応
using UnityEngine;
using UnityEngine.UI;

public class TalkText : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    public void ApplyText(string text)
    {
        _text.text = text;
    }

    public void ClearText()
    {
        _text.text = null;
    }
}