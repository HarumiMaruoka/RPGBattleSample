// 日本語対応

public static class GameManager
{
    private static GameController _gameController = null;

    public static GameController.PlayerActions PlayerActions => _gameController.Player;

    public static void Initialize()
    {
        _gameController = new GameController();
        _gameController.Enable();
    }

    public static void Dispose()
    {
        _gameController = null;
    }
}
