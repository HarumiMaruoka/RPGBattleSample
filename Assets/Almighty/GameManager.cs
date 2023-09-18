// 日本語対応
using UnityEngine;

public class GameManager
{
    private static GameController _gameController = null;

    public static GameController.PlayerActions PlayerActions => _gameController.Player;

    public void Initialize()
    {
        _gameController = new GameController();
    }

    public void Dispose()
    {
        _gameController = null;
    }
}
