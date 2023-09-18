// 日本語対応

// ゲームに必要なデータベースのシングルトン
public class GameDataBase
{
    private readonly static GameDataBase _instance = new GameDataBase();
    public static GameDataBase Instance => _instance;
    private GameDataBase() { }

    private readonly ActorDataBase _actorDataBase = new ActorDataBase();
    private readonly LevelStatusDataBase _levelStatusDataBase = new LevelStatusDataBase();
    private readonly SkillDataBase _skillDataBase = new SkillDataBase();

    public ActorDataBase ActorDataBase => _actorDataBase;
    public LevelStatusDataBase LevelStatusDataStore => _levelStatusDataBase;
    public SkillDataBase SkillDataBase => _skillDataBase;
}
