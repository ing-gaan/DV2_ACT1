using UnityEngine;

///<summary>Controls an atomatic door behaviour</summary>
public class AutomaticDoor : MonoBehaviour
{
    [Header("---------- Event buses ----------")]
    [SerializeField] private InputEventBusScrObj _inputEventBus;
    [SerializeField] private PostMessageEventBusScrObj _postMessageEventBus;
    [Header("---------- Messages ----------")]
    [SerializeField] private ScreenMessagesScrObj _screenMessages;

    private PlayerAccesSecurityManager _playerAccSecManager;
    private Animator _animator;
    private int _openDoorHash;
    private bool _usingAccessCard;
    private int _playerLevelAccces;
    private int _securityLevel; 

    private void OnEnable()
    {
        _inputEventBus.InteractInputEvent += InteractInputEvent;
    }
    private void OnDisable()
    {
        _inputEventBus.InteractInputEvent -= InteractInputEvent;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    ///<summary>Activates when player interact with the door (press the E key)</summary>
    ///<param name="itemName">The item's name</param>
    private void InteractInputEvent(bool interact)
    {
        _usingAccessCard = interact;
    }

    private void Start()
    {      
        _openDoorHash = Animator.StringToHash("openDoor");

        ///Defines the security level of the door
        if (gameObject.name == "AD_ENT_1")
        {
            _securityLevel = 1;
        }
        else if (gameObject.name == "AD_SPW_1")
        {
            _securityLevel = 2;
        }
        else if (gameObject.name == "AD_EXI_1")
        {
            _securityLevel = 3;
        }
        else
        {
            _securityLevel = 0;
        }

    }

    private void OnTriggerEnter(Collider collider)
    {
        ///Defines if player has the security level necessary to access
        if (collider.name == "Player")
        {
            if (_playerAccSecManager == null)
            {
                _playerAccSecManager = collider.GetComponent<PlayerAccesSecurityManager>();
            }

            _playerLevelAccces = _playerAccSecManager.GetMaxPermissionLevel();

            if (_playerLevelAccces > _securityLevel)
            {
                OpenDoor();
            }
            else
            {
                _postMessageEventBus.RaisePostMessageEvent(_screenMessages._interactDoor);
            }            
        }
    }


    private void OnTriggerStay(Collider collider)
    {
        ///Opens door if player interract and has the security level necessary to access
        if (collider.name == "Player" && _usingAccessCard)
        {
            if (_playerLevelAccces >= _securityLevel)
            {
                OpenDoor();
            }
            else
            {
                _postMessageEventBus.RaisePostMessageEvent(_screenMessages._blockedDoor);
            }
        }
    }


    private void OnTriggerExit(Collider collider)
    {
        if (collider.name == "Player")
        {
            CloseDoor();
        }
    }

    ///<summary>Activates the open door animation</summary>
    private void OpenDoor()
    {
        _animator.SetBool(_openDoorHash, true);
    }

    ///<summary>Activates the close door animation</summary>
    private void CloseDoor()
    {
        _animator.SetBool(_openDoorHash, false);
    }
}
