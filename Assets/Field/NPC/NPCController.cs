// 日本語対応
using UnityEngine;
using Field.Talk;
using System.Collections.Generic;

public class NPCController : MonoBehaviour
{
    [SerializeField]
    private TalkData _talkEntryData;
    [SerializeField]
    private List<TalkData> _allTalkData = new List<TalkData>();
}