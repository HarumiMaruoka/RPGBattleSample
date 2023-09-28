// 日本語対応
using UnityEngine;

public class TalkCanvas : MonoBehaviour
{
    private static TalkCanvas _current;
    public static TalkCanvas Current => _current;

    private void OnEnable()
    {
        _current = this;
    }
    private void OnDisable()
    {
        _current = null;
    }

    [SerializeField]
    private TalkText _talkText;
    [SerializeField]
    private TalkSelecter _talkSelecter;

    public TalkText TalkText => _talkText;
    public TalkSelecter TalkSelecter => _talkSelecter;
}