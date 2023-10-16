// 日本語対応
using System.Collections.Generic;
using Sky.Actor;
using UnityEngine;

namespace Sky
{
    public class ActorDataBase
    {
        private readonly List<ActorModel> _allActorList = new List<ActorModel>();
        private readonly Dictionary<int, ActorModel> _idToActor = new Dictionary<int, ActorModel>();
        private readonly Dictionary<string, ActorModel> _nameToActor = new Dictionary<string, ActorModel>();

        public IReadOnlyList<ActorModel> AllActorList => _allActorList;
        public IReadOnlyDictionary<int, ActorModel> IDToActor => _idToActor;
        public IReadOnlyDictionary<string, ActorModel> NameToActor => _nameToActor;

        public void Initialize(List<string[]> csvData)
        {
            for (int i = 0; i < csvData.Count; i++)
            {
                int id = int.Parse(csvData[i][0]); // ActorIDの取得
                string name = csvData[i][1];       // 名前の取得

                // Actorの生成
                var actor = new ActorModel(id, name);

                // 各コレクションへ登録
                _allActorList.Add(actor);
                _idToActor.Add(id, actor);
                _nameToActor.Add(name, actor);
            }

            foreach (var actor in _allActorList)
            {
                //actor.Initialize();
            }
        }

        public ActorModel GetRandomActorModel()
        {
            // ランダムにActorModelを選択
            int randomIndex = Random.Range(0, _allActorList.Count);
            return _allActorList[randomIndex].Clone();
        }
    }
}