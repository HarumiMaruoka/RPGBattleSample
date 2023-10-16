// 日本語対応
using Sky.Skill;
using System.Collections.Generic;

public class SkillDataBase
{
    private readonly List<SkillBasicData> _allSkillCollection = new List<SkillBasicData>();
    private readonly Dictionary<int, SkillBasicData> _idToSkill = new Dictionary<int, SkillBasicData>();
    private readonly Dictionary<string, SkillBasicData> _nameToSkill = new Dictionary<string, SkillBasicData>();

    public IReadOnlyList<SkillBasicData> AllSkillList => _allSkillCollection;
    public IReadOnlyDictionary<int, SkillBasicData> IDToSkill => _idToSkill;
    public IReadOnlyDictionary<string, SkillBasicData> NameToSkill => _nameToSkill;

    public void Initialize(List<string[]> csvData)
    {
        for (int i = 0; i < csvData.Count; i++)
        {
            int id = int.Parse(csvData[i][0]); // ActorIDの取得
            string name = csvData[i][1];       // 名前の取得

            // Skillを生成する。
            var skill = SkillBasicData.CreateSkill(csvData[i]);

            // 各コレクションへ登録する。
            _allSkillCollection.Add(skill);
            _idToSkill.Add(id, skill);
            _nameToSkill.Add(name, skill);
        }

        foreach (var actor in _allSkillCollection)
        {
            //actor.Initialize();
        }
    }
}