// 日本語対応
using UnityEngine;
using Glib.InspectorExtension;
using Glib.HitSupport;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField, InputName]
    private string _horizontalInputName;
    [SerializeField, InputName]
    private string _verticalInputName;
    [SerializeField, InputName]
    private string _jumpInputName;
    [SerializeField]
    private float _maxHorizontalSpeed = 24f;
    [SerializeField]
    private float _horizontalAcceleration = 12f;
    [SerializeField]
    private float _horizontalDeceleration = 12f;
    [SerializeField]
    private float _jumpPower = 8f;
    [SerializeField]
    private float _groundedGravity = 200f;
    [SerializeField]
    private float _midairGravity = 18f;
    [SerializeField]
    private SphereCast _groundedChecker;
    [SerializeField]
    private float _rotationSpeed = 600f;

    [SerializeField]
    private Camera _criterionCamera; // 回転の基準となるカメラ
    [SerializeField]
    private Transform _groundedCheckerCriterion; // 接地判定用レイの基準点

    private CharacterController _characterController = null;
    private PlayerStateController _playerStateController = null;

    private bool CanMove
    {
        get
        {
            bool result =
                !_playerStateController.HasState(FieldPlayerState.Talk); // 会話中は移動できない。

            return result;
        }
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerStateController = GetComponent<PlayerStateController>();
    }
    private void Update()
    {
        _playerStateController.RemoveState(FieldPlayerState.Run);
        _playerStateController.RemoveState(FieldPlayerState.Rise);
        _playerStateController.RemoveState(FieldPlayerState.Fall);

        float inputX = 0f;
        float inputY = 0f;
        bool inputJump = false;

        if (CanMove)
        {
            inputX = Input.GetAxisRaw(_horizontalInputName);
            inputY = Input.GetAxisRaw(_verticalInputName);
            inputJump = Input.GetButtonDown(_jumpInputName);
        }


        Vector3 moveVector = HorizontalCalculation(_maxHorizontalSpeed, new Vector2(inputX, inputY), _horizontalAcceleration, _horizontalDeceleration);
        moveVector.y = VerticalCalculation(_groundedGravity, _midairGravity, IsGrounded, inputJump, _jumpPower);

        bool isRun = new Vector2(moveVector.x, moveVector.z).sqrMagnitude > 0.001f;
        bool isRise = moveVector.y > 0.001f;
        bool isFall = moveVector.y < 0.001f;

        if (isRun) _playerStateController.AddState(FieldPlayerState.Run);
        if (!IsGrounded && isRise) _playerStateController.AddState(FieldPlayerState.Rise);
        if (!IsGrounded && isFall) _playerStateController.AddState(FieldPlayerState.Fall);

        _characterController.Move(moveVector);
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        _groundedChecker.OnDrawGizmos(_groundedCheckerCriterion);
    }
#endif

    private float _currentHorizontalSpeed = 0f;
    private Vector2 _lastDir = Vector2.zero;
    private Quaternion _targetRotation;
    private Vector3 _vector;
    public Vector3 HorizontalCalculation(float maxSpeed, Vector2 dir, float acceleration, float deceleration)
    {
        // 入力があるとき
        if (dir.sqrMagnitude > 0.001f)
        {
            // 速度を加算していく
            _currentHorizontalSpeed += Time.deltaTime * acceleration;
            // 最大速度を超えないようにする
            if (maxSpeed < _currentHorizontalSpeed) _currentHorizontalSpeed = maxSpeed;

            // 方向の更新を行う
            _lastDir = dir.normalized;
        }
        // 入力が無いとき
        else
        {
            // 速度を減算させる
            _currentHorizontalSpeed -= Time.deltaTime * deceleration;
            // 速度を負の数にならないようにする。
            if (_currentHorizontalSpeed < 0f) _currentHorizontalSpeed = 0f;
        }

        var horizontalRotation = Quaternion.AngleAxis(_criterionCamera.transform.eulerAngles.y, Vector3.up);
        _vector = horizontalRotation * new Vector3(_lastDir.x, 0f, _lastDir.y) * _currentHorizontalSpeed;

        // 移動方向を向く
        if (_vector != Vector3.zero)
        {
            _targetRotation = Quaternion.LookRotation(_vector, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);
        }

        return _vector * Time.deltaTime;
    }
    private float _currentVerticalSpeed = 0f;
    private bool _preIsGrounded;
    public float VerticalCalculation(float groundedGravity, float gravity,
        bool isGrounded, bool isJump, float JumpPower)
    {
        // 接地しているとき
        if (isGrounded)
        {
            // ジャンプする場合
            if (isJump)
            {
                _currentVerticalSpeed = JumpPower;
            }
            // ジャンプしない場合
            else if (_currentVerticalSpeed < 0f)
            {
                _currentVerticalSpeed = -groundedGravity;
            }
        }
        // 接地していないとき
        else
        {
            if (_preIsGrounded && _currentVerticalSpeed < 0f)
            {
                _currentVerticalSpeed = 0f;
            }
            else
            {
                _currentVerticalSpeed -= gravity * Time.deltaTime;
            }
        }
        _preIsGrounded = isGrounded;
        return _currentVerticalSpeed * Time.deltaTime;
    }

    public float CurrentHorizontalSpeed => _currentHorizontalSpeed;
    public float CurrentVerticalSpeed => _currentVerticalSpeed;
    public float MaxHorizontalSpeed => _maxHorizontalSpeed;
    public bool IsGrounded => _groundedChecker.IsHit(_groundedCheckerCriterion);
}