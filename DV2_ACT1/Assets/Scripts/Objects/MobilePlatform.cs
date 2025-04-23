using System.Collections;
using UnityEngine;

///<summary>Controls an mobile platform</summary>
public class MobilePlatform : MonoBehaviour
{
    [Header("---------- Event buses ----------")]
    [SerializeField] private InputEventBusScrObj _inputEventBus;
    [SerializeField] private PostMessageEventBusScrObj _postMessageEventBus;

    [Header("---------- Default positions ----------")]
    [SerializeField] private Transform _endHorizontal;
    [SerializeField] private Transform _endVertical;
    [SerializeField] private GameObject _elevatorDoorFront;
    [SerializeField] private GameObject _elevatorDoorBack;

    [Header("---------- Messages ----------")]
    [SerializeField] private ScreenMessagesScrObj _screenMessages;

    private Rigidbody _rb;
    
    private IEnumerator _moveCoroutine;
    private CharacterController _playerController;

    private Vector3 _originPosition;
    private Vector3 _currentPosition;
    private Vector3 _startPosition;
    private Vector3 _endPosition;

    private bool _playerIsOn = false;
    private bool _move = false;
    private enum Travel
    {
        VERTICAL, HORIZONTAL
    }


    private void OnEnable()
    {
        _inputEventBus.MoveVerticalInputEvent += MoveVerticalInputEvent;
        _inputEventBus.MoveHorizontalInputEvent += MoveHorizontalInputEvent;
    }

    private void OnDisable()
    {
        _inputEventBus.MoveVerticalInputEvent -= MoveVerticalInputEvent;
        _inputEventBus.MoveHorizontalInputEvent -= MoveHorizontalInputEvent;
    }


    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _originPosition = transform.position;
        _currentPosition = _originPosition;
    }

    ///<summary>Moves the platform vertically when player interact (press the V key)</summary>
    private void MoveVerticalInputEvent()
    {
        if (_playerIsOn && DefinePositions(Travel.VERTICAL))
        {
            _move = true;
            _moveCoroutine = MovePlatform(10f);
            StartCoroutine(_moveCoroutine);
        }
    }

    ///<summary>Moves the platform horizontally when player interact (press the H key)</summary>
    private void MoveHorizontalInputEvent ()
    {      
        if (_playerIsOn && DefinePositions(Travel.HORIZONTAL))
        {
            _move = true;
            _moveCoroutine = MovePlatform(2f);
            StartCoroutine(_moveCoroutine);
        }
    }

    ///<summary>Defines the platform position to sets a possible end position</summary>
    private bool DefinePositions(Travel travel)
    {
        _startPosition = _currentPosition;
        if (_currentPosition == _originPosition)
        {
            if (travel == Travel.VERTICAL)
            {
                _endPosition = _endVertical.position;
            }
            else
            {
                _endPosition = _endHorizontal.position;
            }
            return true;
        }
        else if (_currentPosition == _endVertical.position)
        {
            if (travel == Travel.VERTICAL)
            {
                _endPosition = _originPosition;
                return true;
            }
        }
        else if (_currentPosition == _endHorizontal.position)
        {
            if (travel == Travel.HORIZONTAL)
            {
                _endPosition = _originPosition;
                return true;
            }
        }
        return false;
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (_playerController == null)
            {
                _playerController = collider.GetComponent<CharacterController>();
            }
            _postMessageEventBus.RaisePostMessageEvent(_screenMessages._interactElevator);

            _playerIsOn = true;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        ///Sends the platform velocity to the player
        if (collider.CompareTag("Player"))
        {
            _playerController.Move(_rb.linearVelocity * Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            _playerIsOn = false;
        }
    }

    ///<summary>Moves the platform at the specified speed</summary>
    ///<param name="speed">The specified speed</param>
    private IEnumerator MovePlatform(float speed)
    {
        float traveled = 0;
        _elevatorDoorFront.SetActive(true);
        _elevatorDoorBack.SetActive(true);
        while (_move)
        {
            _currentPosition = Vector3.Lerp(_startPosition, _endPosition, traveled);
            _rb.MovePosition(_currentPosition);
            traveled += 0.001f * speed;

            if (_currentPosition == _endPosition)
            {
                _move = false;
            }
            yield return new WaitForEndOfFrame();
        }
        if (_endPosition == _originPosition)
        {
            _elevatorDoorBack.SetActive(false);
        }
        else
        {
            _elevatorDoorFront.SetActive(false);
        }
            
        yield return null;
    }

}
