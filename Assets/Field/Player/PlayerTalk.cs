// 日本語対応
using UnityEngine;
using Glib.HitSupport;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;

public class PlayerTalk : MonoBehaviour
{
    [SerializeField]
    private Raycast _raycast;

    private PlayerStateController _playerStateController = null;

    private void Start()
    {
        _playerStateController = GetComponent<PlayerStateController>();
    }

    private void OnEnable()
    {
        GameManager.PlayerActions.Decision.started += Talk;
    }
    private void OnDisable()
    {
        GameManager.PlayerActions.Decision.started -= Talk;
    }

    private void Talk(InputAction.CallbackContext context)
    {
        if (CanTalk && TryGetTalkable(out ITakable takable))
        {
            // 会話ステートを追加。
            _playerStateController.AddState(FieldPlayerState.Talk);

            // 会話開始。会話終了時に会話ステートを削除。
            takable.BeginTalk(this.GetCancellationTokenOnDestroy(),
                () => _playerStateController.RemoveState(FieldPlayerState.Talk));
        }
    }

    private void Update()
    {
        // 入力があり、会話可能な状態であり、会話可能オブジェクトを取得できた場合、会話開始。

    }

    private bool CanTalk
    {
        get
        {
            bool result =
                !_playerStateController.HasState(FieldPlayerState.Talk); // すでに会話中の場合は無効。

            return result;
        }
    }

    private bool TryGetTalkable(out ITakable takable)
    {
        var hitCol = _raycast.GetHitInfo(transform).collider;

        if (hitCol)
        {
            hitCol.TryGetComponent(out takable);
        }
        else
        {
            takable = null;
        }

        return takable != null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        _raycast.OnDrawGizmos(transform);
    }
#endif
}