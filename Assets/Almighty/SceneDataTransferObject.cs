// 日本語対応

using System.Collections.Generic;

public class SceneDataTransferObject // シーン間でデータを共有する用のシングルトン
{
    private readonly static SceneDataTransferObject _instance = new SceneDataTransferObject();
    public static SceneDataTransferObject Instance => _instance;
    private SceneDataTransferObject() { }

    public IReadOnlyList<int> EncounteredEnemyIDs { get; set; } // 遭遇した敵のIDリスト
}