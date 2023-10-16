// 日本語対応
using Sky.Battle;
using UnityEngine;
using UnityEngine.UI;

public class BattleActorView : MonoBehaviour
{
    [SerializeField]
    private Text _actorInformationText;

    private BattleActorModel _actor;

    public void Initilize(BattleActorModel actor)
    {
        _actor = actor;
    }
    public void Update()
    {
        _actorInformationText.text = _actor.ToString();
    }
}
