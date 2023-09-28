// 日本語対応
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace Field
{
    namespace Talk
    {
        public class TalkSelecter
        {
            public async UniTask<Glib.Talk.Node> GetSelectItemAsync(TalkDataController talkDataController, CancellationToken token)
            {
                try
                {
                    while (!token.IsCancellationRequested)
                    {
                        await UniTask.Yield(token);
                        var input = GameManager.PlayerActions;

                        if (input.Next.WasPerformedThisFrame()) talkDataController.NextInput();
                        if (input.Previous.WasPerformedThisFrame()) talkDataController.PrevInput();
                        if (input.Decision.WasPerformedThisFrame())
                        {
                            var selectItem = talkDataController.HoverItem;
                            talkDataController.Step();
                            return selectItem;
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    UnityEngine.Debug.Log("Cancelled");
                }
                return null;
            }
        }
    }
}