// 日本語対応
using UnityEngine;
using UnityEngine.SceneManagement;
using Glib.InspectorExtension;

// バトルシーンへの遷移処理を中心に担当するクラス。
// フィールドのプレイヤーにアタッチすることを想定して作成したクラス。
public class BattleTransitionHandler : MonoBehaviour
{
    [SerializeField, TagName]
    private string _enemyTagName;
    [SerializeField, SceneName]
    private string _battleSceneName;

    // フィールドモード開始時に遭遇敵リストを破棄。
    private void Start()
    {
        SceneDataTransferObject.Instance.EncounteredEnemyIDs = null;
    }

    // フィールドのエネミーに接触したときにバトルシーンに遷移。
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.TryGetComponent(out FieldEnemyController enemy))
        {
            // SceneDataTransferObjectにどの敵と接触したか教える。
            SceneDataTransferObject.Instance.EncounteredEnemyIDs = enemy.EncounteredEnemyIDs;

            // バトルシーンに遷移する。
            SceneManager.LoadScene(_battleSceneName);
        }
    }
}