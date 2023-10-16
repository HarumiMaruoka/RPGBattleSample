// 日本語対応
using UnityEngine;
using Sky.DataBase;

[DefaultExecutionOrder(-100)]
public class GameInitializer : MonoBehaviour
{
    [SerializeField]
    private TextAsset _actorDataCsv;
    [SerializeField]
    private TextAsset _levelDataCsv;
    [SerializeField]
    private TextAsset _skillDataCsv;

    private static bool _isInitialized = false;

    private void Awake()
    {
        if (_isInitialized) return;
        _isInitialized = true;

        GameManager.Initialize();
        // データ読み込み。逐次的処理をしている所があるので順番を崩さないように！
        GameDataBase.Instance.LevelStatusDataStore.Initialize(CSVReader.ParseCsv(_levelDataCsv.text, 1));
        GameDataBase.Instance.ActorDataBase.Initialize(CSVReader.ParseCsv(_actorDataCsv.text, 1));
        GameDataBase.Instance.SkillDataBase.Initialize(CSVReader.ParseCsv(_skillDataCsv.text, 7));
    }
}
