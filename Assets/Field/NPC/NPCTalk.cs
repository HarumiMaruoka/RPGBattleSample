// 日本語対応
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(TalkDataController))]
public class NPCTalk : MonoBehaviour
{
    private TalkDataController _talkController;

    private void Start()
    {
        _talkController = GetComponent<TalkDataController>();
    }

    public async void TalkAsync(CancellationToken token, Action onEndTalk)
    {
        IReadOnlyList<Glib.Talk.Node> currents = null;
        currents = _talkController.Currents;

        while (currents != null && currents.Count != 0)
        {
            try
            {
                if (currents.Count == 1)
                {
                    var text = currents[0].Text;
                    TalkCanvas.Current.TalkText.ApplyText(text);
                }
                else
                {
                    TalkCanvas.Current.TalkSelecter.CreateSelectables(currents);
                }

                await UniTask.WaitUntil(
                    GameManager.PlayerActions.Decision.IsPressed,
                    cancellationToken: token);

                _talkController.Step();
                currents = _talkController.Currents;
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Cancelled");
            }
        }
        onEndTalk();
    }
}