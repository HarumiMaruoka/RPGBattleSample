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
    private GameObject _uiRootObject;
    [SerializeField]
    private TalkText _talkText;
    [SerializeField]
    private TalkSelecter _talkSelecter;

    public GameObject UIRootObject => _uiRootObject;
    public TalkText TalkText => _talkText;
    public TalkSelecter TalkSelecter => _talkSelecter;
}