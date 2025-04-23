using UnityEngine;
using UnityEngine.AI;

///<summary>Class to defines the general state behaviour</summary>
[RequireComponent(typeof(PlayerDetector))]
public abstract class BaseState : MonoBehaviour
{
    [Header("---------- Event buses ----------")]
    [SerializeField] protected EnemyEventBusScrObj _enemyEventBus;
    [Header("---------- Settings ----------")]
    [SerializeField] protected EnemySettingsScrObj _enemySettings;

    private PlayerDetector _playerDetector;
    protected NavMeshAgent _enemyNavAgent;

    protected bool _playerDetected = false;
    protected bool _playerListened = false;

    protected Vector3 _playerPosition;
    protected Vector3 _lastPlayerPosition;
    protected float _distanceToPlayer;

    protected int _velocityHash;
    protected int _attackHash;
    protected int _rotateHash;

    protected Animator _animator;
    protected ParticleSystem _shotParticleSystem;


    protected virtual void Awake()
    {
        _playerDetector = GetComponent<PlayerDetector>();
        _enemyNavAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _shotParticleSystem = GetComponentInChildren<ParticleSystem>();
        
        _animator.speed = 1.5f;
        _shotParticleSystem.Stop();

        _velocityHash = Animator.StringToHash("velocity");
        _attackHash = Animator.StringToHash("attack");
        _rotateHash = Animator.StringToHash("rotate");        
    }

    protected virtual void Update()
    {
        PlayerDetected();

        _animator.SetFloat(_velocityHash, Mathf.Abs(_enemyNavAgent.velocity.magnitude));

        if (_playerDetected)
        {
            _distanceToPlayer = Vector3.Distance(transform.position, _playerPosition);
        }
    }


    ///<summary>Listens the player detection event</summary>
    ///<param name="detected">True if the player was detected. False if not</param>
    ///<param name="position">The position of the player</param>
    protected virtual void PlayerDetectedAction(bool detected, Vector3 position)
    {
        _playerDetected = detected;
      
        if (_playerDetected)
        {
            _playerPosition = position;
        }
        else
        {
            _lastPlayerPosition = _playerPosition;
        }
    }

    ///<summary>Listens the player proximity event</summary>
    ///<param name="listened">True if the player was listened. False if not</param>
    protected virtual void PlayerListenedAction(bool listened)
    {
        _playerListened = listened;
    }

    ///<summary>Change state</summary>
    ///<param name="state">The destination state</param>
    protected virtual bool ChangeState(BaseState state)
    {
        if (state != null)
        {
            state.enabled = true;
            enabled = false;
            return true;
        }
        return false;
    }

    ///<summary>Checks if detector has detected or listened to the player</summary>
    private void PlayerDetected()
    {
        bool detected;
        Vector3 position;
        bool listened;

        _playerDetector.PlayerDetected(out detected, out position);
        _playerDetector.PlayerListened(out listened);

        PlayerDetectedAction(detected, position);
        PlayerListenedAction(listened);
    }
}
