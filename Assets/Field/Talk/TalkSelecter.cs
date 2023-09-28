// 日本語対応
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;


namespace Field
{
    namespace Talk
    {
        public class TalkSelecter
        {
            private int _index = 0;

            private IReadOnlyList<TalkData> _selectables;
            public IReadOnlyList<TalkData> Selectables => _selectables;

            public TalkData HoverItem => _selectables[_index];

            public void Initialize(IReadOnlyList<TalkData> selectables)
            {
                _selectables = selectables;
            }
            public void Next()
            {
                _selectables[_index].OnUnhover();
                _index++;
                if (_index == _selectables.Count) _index = 0;
                _selectables[_index].OnHover();
            }
            public void Previous()
            {
                _selectables[_index].OnUnhover();
                _index--;
                if (_index == -1) _index = _selectables.Count - 1;
                _selectables[_index].OnHover();
            }
            public TalkData Select()
            {
                return HoverItem;
            }

            public async UniTask<TalkData> GetSelectItemAsync(CancellationToken token)
            {
                try
                {
                    while (!token.IsCancellationRequested)
                    {
                        var input = GameManager.PlayerActions;

                        if (input.Next.IsPressed()) Next();
                        if (input.Previous.IsPressed()) Previous();
                        if (input.Decision.IsPressed()) return HoverItem;

                        await UniTask.Yield(token);
                    }
                }
                catch (OperationCanceledException)
                {
                    // 待機がキャンセルされた。
                }
                return null;
            }
        }
    }
}