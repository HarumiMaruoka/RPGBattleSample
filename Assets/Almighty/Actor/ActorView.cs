// 日本語対応
using UnityEngine;
using Sky.Actor;

namespace Sky
{
    public class ActorView : MonoBehaviour
    {
        private ActorModel _actor = null;

        public void Initialize(ActorModel model)
        {
            _actor = model;
        }
    }
}