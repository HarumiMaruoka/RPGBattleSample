// 日本語対応
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Field
{
    namespace Talk
    {
        public class TalkManager
        {
            private readonly TalkSelecter _talkSelecter = new TalkSelecter();
            private TalkData _current;
            private TalkStatus _status;

            public TalkSelecter TalkSelecter => _talkSelecter;
            public TalkData Current => _current;
            public TalkStatus Status => _status;

            public async void StartTalk(TalkData start, CancellationToken token)
            {
                _status = TalkStatus.Talk;
                _current = start;

                while (_current != null)
                {
                    // 決定入力があるまで待機。
                    var decisionInput = GameManager.PlayerActions.Decision;
                    await UniTask.WaitUntil(decisionInput.IsPressed, cancellationToken: token);

                    // 決定入力が発生したら進行。
                    // 次の会話データがある場合。
                    if (_current.Nexts != null && _current.Nexts.Count != 0)
                    {
                        // 選択肢が単一の場合。
                        if (_current.Nexts.Count == 1)
                        {
                            _status = TalkStatus.Talk;
                            _current = _current.Nexts[0];
                        }
                        // 選択肢が複数の場合。
                        else
                        {
                            _status = TalkStatus.Choose;
                            _talkSelecter.Initialize(_current.Nexts); // 選択肢を渡す。
                            _current = await _talkSelecter.GetSelectItemAsync(token); // ユーザーが選択肢から選ぶまで待機。
                        }
                    }
                    // 次の会話データがない場合。
                    else
                    {
                        _status = TalkStatus.None;
                        _current = null;
                    }
                }
            }
        }
        public enum TalkStatus
        {
            None,
            Talk,
            Choose,
        }
    }
}