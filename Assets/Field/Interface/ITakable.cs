// 日本語対応
using System;
using System.Threading;

public interface ITakable
{
    public void BeginTalk(CancellationToken token, Action onEndTalk);
}