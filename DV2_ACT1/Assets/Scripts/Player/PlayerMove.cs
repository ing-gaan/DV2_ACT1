using UnityEngine;

///<summary>Controls the player movement</summary>
public class PlayerMove : MonoBehaviour
{
    [Header("---------------  Event buses ---------------")]
    [SerializeField] private InputEventBusScrObj _inputEventBus;

    [Header("---------------  Settings  ------------------")]
    [SerializeField] private GameSettingsScrObj _gameSettings;

    [Header("---------------  Params  --------------------")]
    [SerializeField] private float _maxWalkSpeed = 5f;
    [SerializeField] private float _maxRunSpeed = 10f;
    //[SerializeField] private float _fallTimeout = 0.15f;
    [SerializeField] private float _jumpHeight = 1.2f;
    [SerializeField] private float _gravity = -15.0f;
    [SerializeField] private Transform _camera;

    private CharacterController _chController;
    //private AudioSource _audioSource;

    private Vector3 _inputDirection;
    private Vector3 _moveDirection;

    private float _initVerticalVelocity;
    private float _verticalVelocity = 0;
    private float _terminalVelocity = 53.0f;
    private float _maxCurrentSpeed = 0;

    private bool _isMoving = false;
    private bool _isRunning = false;
    private bool _isJumping = false;
    private bool _isInAir = false;


    private void OnEnable()
    {
        _inputEventBus.MoveInputEvent += MoveInputEvent;
        _inputEventBus.RunInputEvent += RunInputEvent;
        _inputEventBus.JumpInputEvent += JumpInputEvent;
    }

    private void OnDisable()
    {
        _inputEventBus.MoveInputEvent -= MoveInputEvent;
        _inputEventBus.RunInputEvent -= RunInputEvent;
        _inputEventBus.JumpInputEvent -= JumpInputEvent;
    }


    private void MoveInputEvent(Vector2 direction)
    {
        if (direction.sqrMagnitude > 0)
        {
            _inputDirection = new Vector3(direction.x, 0, direction.y);
            _isMoving = true;
            _maxCurrentSpeed = _maxWalkSpeed;
            return;
        }
        _isMoving = false;
        _maxCurrentSpeed = 0;
    }

    private void RunInputEvent(bool runInput)
    {
        if (!_isMoving)
        {
            return;
        }

        _isRunning = runInput;
        if (_isRunning)
        {
            _maxCurrentSpeed = _maxRunSpeed;
            return;
        }
        _maxCurrentSpeed = _maxWalkSpeed;
    }

    private void JumpInputEvent()
    {
        if (!_isJumping && !_isInAir)
        {
            _isJumping = true;
            _verticalVelocity = _initVerticalVelocity;
        }
    }


    void Start()
    {
        _chController = GetComponent<CharacterController>();
        //_audioSource = GetComponent<AudioSource>();
        _initVerticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
    }

    private void Update()
    {
        JumpAndGravity();
        MovePlayer();
    }


    private void MovePlayer()
    {
        _moveDirection = _camera.right * _inputDirection.x + _camera.forward * _inputDirection.z;
        _moveDirection.y = 0;

        Vector3 moveVector = _moveDirection * _maxCurrentSpeed * Time.deltaTime * (_isMoving ? 1 : 0);
        Vector3 jumpVector = new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime;
        _chController.Move(moveVector + jumpVector);

        if (_moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(_moveDirection);
        }

        Vector2 hVelocity = new Vector2(_chController.velocity.x, _chController.velocity.z);
    }


    private void JumpAndGravity()
    {
        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += _gravity * Time.deltaTime;
        }

        if (_chController.isGrounded)
        {
            if (_verticalVelocity < 0f)
            {
                _verticalVelocity = -1f;
            }

            if (_isInAir)
            {
                _isJumping = false;
                _isInAir = false;
            }
        }
        else
        {
            _isInAir = true;
        }
    }

}
