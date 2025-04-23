using UnityEngine;

///<summary>Detects the player using a Field Of View</summary>
public class PlayerDetector : MonoBehaviour
{
    [Header("---------- Settings ----------")]
    [SerializeField] private EnemySettingsScrObj _enemySettings;

    private Transform _player;

    private float _maxFOVDistance;
    private float _currentFOVDistance;

    private Vector3 _vectorToPlayer;
    private Vector3 _origin;
    private Vector3 _direction;
    private bool _hit;
    private RaycastHit _hitInfo;
    private int _layerMask;

    private bool _playerDetected = false;
    private bool _playerListened = false;


    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();
    }
    void Start()
    {
        _layerMask = LayerMask.GetMask("Enviroment");
    }

    void Update()
    {
        FOVCollisionDetection();
        PlayerDetection();
    }

    ///<summary>Detects the FOV collisions to corrects the FOV distance</summary>
    private void FOVCollisionDetection()
    {
        _origin = transform.position + new Vector3(0f, 0.5f, 0f);
        _direction = transform.forward;
        _hit = Physics.Raycast(_origin, _direction, out _hitInfo, _maxFOVDistance, _layerMask);
        Debug.DrawRay(_origin, _direction * _maxFOVDistance, Color.yellow);
        if (_hit)
        {
            _currentFOVDistance = _hitInfo.distance;
        }
        else
        {
            _maxFOVDistance = _enemySettings.GetFOVDistance();
            _currentFOVDistance = _maxFOVDistance;
        }
    }

    ///<summary>Detects if player is into the FOV</summary>
    private void PlayerDetection()
    {
        _vectorToPlayer = _player.position - transform.position;

        ///Checks if player is within the FOVAngle range and view distance range
        if (Vector3.Angle(transform.forward, _vectorToPlayer) <= _enemySettings.GetFOVAngle() && _vectorToPlayer.magnitude <= _currentFOVDistance)
        {
            _playerDetected = true;            
        }
        else
        {
            _playerDetected = false;   
        }

        Listen();
    }

    ///<summary>Check if player is close</summary>
    private void Listen()
    {
        if (_vectorToPlayer.magnitude < _enemySettings.GetListenDistance())
        {
            _playerListened = true;
        }
        else
        {
            _playerListened = false;
        }
    }

    ///<summary>Lets to inform the detection state</summary>
    public void PlayerDetected(out bool detected, out Vector3 position)
    {
        detected = _playerDetected;
        position = _player.position;
    }

    ///<summary>Lets to inform the listen state</summary>
    public void PlayerListened(out bool listened)
    {
        listened = _playerListened;
    }

}
