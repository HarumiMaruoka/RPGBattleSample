// 日本語対応
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(TalkDataController))]
public class NPCTalk : MonoBehaviour, ITakable
{
    private TalkDataController _talkController;
    Field.Talk.TalkSelecter _talkSelecter = new Field.Talk.TalkSelecter();

    private void Start()
    {
        _talkController = GetComponent<TalkDataController>();
        TalkCanvas.Current.UIRootObject.SetActive(false);
    }

    public async void BeginTalk(CancellationToken token, Action onEndTalk)
    {
        TalkCanvas.Current.UIRootObject.SetActive(true);
        IReadOnlyList<Glib.Talk.Node> currents = null;
        currents = _talkController.Currents;

        while (currents != null && currents.Count != 0)
        {
            try
            {
                if (!TalkCanvas.Current) return;

                TalkCanvas.Current.TalkText.ClearText();
                TalkCanvas.Current.TalkSelecter.DisposeSelectables();

                if (currents.Count == 1)
                {
                    var text = currents[0].Text;
                    TalkCanvas.Current.TalkText.ApplyText(text);

                    await UniTask.Yield(cancellationToken: token);
                    await UniTask.WaitUntil(
                        GameManager.PlayerActions.Decision.WasPressedThisFrame,
                        cancellationToken: token);

                    _talkController.TryStep(out currents);
                }
                else
                {
                    TalkCanvas.Current.TalkSelecter.CreateSelectables(currents);
                    var selectItem = await _talkSelecter.GetSelectItemAsync(_talkController, token);
                    currents = selectItem.NextNodes;
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Cancelled");
                return;
            }
        }
        onEndTalk();
        TalkCanvas.Current.UIRootObject.SetActive(false);
        _talkController.ResetCurrents();
    }
}