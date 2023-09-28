// 日本語対応
using UnityEngine;
using Glib.InspectorExtension;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField, AnimationParameter]
    private string _horizontalMoveSpeedAnimParamName;
    [SerializeField, AnimationParameter]
    private string _isGroundedAnimParamName;

    private Animator _animator; // プレイヤーのアニメーター
    private PlayerMove _playerMove; // プレイヤーの移動制御クラス。

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerMove = GetComponent<PlayerMove>();
    }
    private void Update()
    {
        float speed = _playerMove.CurrentHorizontalSpeed / _playerMove.MaxHorizontalSpeed;
        _animator.SetFloat(_horizontalMoveSpeedAnimParamName, speed);

        _animator.SetBool(_isGroundedAnimParamName, _playerMove.IsGrounded);
    }
}