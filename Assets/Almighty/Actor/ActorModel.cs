// 日本語対応

namespace Actor
{
    public class ActorModel
    {
        public ActorModel(int id, string name)
        {
            _id = id; _name = name;
            _actorLevelStatus = new ActorLevelStatus(10000);
            _resourceStatus = new ActorResourceStatus(id, _actorLevelStatus);
            _actorSkill = new ActorSkillData();
            _statusEffect = new ActorStatusEffect();

            _resourceStatus.RestoreToMaxValues();
        }

        private readonly int _id;
        private readonly string _name;

        private ActorLevelStatus _actorLevelStatus;
        private ActorResourceStatus _resourceStatus;
        private ActorSkillData _actorSkill;
        private ActorStatusEffect _statusEffect;

        public int ID => _id;
        public string Name => _name;
        public ActorLevelStatus LevelStatus => _actorLevelStatus;
        public ActorResourceStatus ResourceStatus => _resourceStatus;
        public ActorSkillData SkillData => _actorSkill;
        public ActorStatusEffect StatusEffect => _statusEffect;

        public ActorAttributeStatus LevelAttributeStatus =>
            GameDataBase.Instance.LevelStatusDataStore.StatusData[new LevelData(_id, LevelStatus.Level)];
        public ActorAttributeStatus TotalAttributeStatus => LevelAttributeStatus;

        public bool IsDead { get => _resourceStatus.IsDead; }

        public void Damage(int damageValue)
        {
            _resourceStatus.Damage(damageValue);
        }

        // 自身の複製を返す。
        // 敵と遭遇した際、敵はデータベースからコピーする為。
        public ActorModel Clone()
        {
            // ActorModelクラスを新しいインスタンスにコピー
            ActorModel clone = new ActorModel(_id, _name);

            // ActorLevelStatus、ActorResourceStatus、ActorSkillData、ActorStatusEffectをディープコピー
            clone._actorLevelStatus = _actorLevelStatus.Clone();
            clone._resourceStatus = _resourceStatus.Clone(_actorLevelStatus);
            clone._actorSkill = _actorSkill.Clone();
            clone._statusEffect = _statusEffect.Clone();

            return clone;
        }

        public override string ToString()
        {
            return $"Name: {_name},ID: {_id}, HP: {_resourceStatus.Hp},MP: {_resourceStatus.Mp}";
        }
    }
}