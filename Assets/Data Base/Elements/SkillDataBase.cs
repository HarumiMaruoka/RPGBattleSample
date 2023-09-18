// 日本語対応
using Skill;
using System.Collections.Generic;

public class SkillDataBase
{
    private readonly List<SkillModel> _allSkillCollection = new List<SkillModel>();
    private readonly Dictionary<int, SkillModel> _idToSkill = new Dictionary<int, SkillModel>();
    private readonly Dictionary<string, SkillModel> _nameToSkill = new Dictionary<string, SkillModel>();

    public IReadOnlyList<SkillModel> AllSkillList => _allSkillCollection;
    public IReadOnlyDictionary<int, SkillModel> IDToSkill => _idToSkill;
    public IReadOnlyDictionary<string, SkillModel> NameToSkill => _nameToSkill;

    public void Initialize(List<string[]> csvData)
    {
        for (int i = 0; i < csvData.Count; i++)
        {
            int id = int.Parse(csvData[i][0]); // ActorIDの取得
            string name = csvData[i][1];       // 名前の取得

            // Skillを生成する。
            var skill = SkillModel.CreateSkill(csvData[i]);

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