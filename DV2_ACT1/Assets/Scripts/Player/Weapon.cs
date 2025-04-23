using UnityEngine;

///<summary>Controls the player's current weapon</summary>
public class Weapon : MonoBehaviour
{
    [Header("---------- Event buses ----------")]
    [SerializeField] private InputEventBusScrObj _inputEventBus;
    [SerializeField] private PlayerManagerEventBusScrObj _playerManagerEventBus;
    [SerializeField] private WeaponEventBusScrObj _weaponEventBus;

    private Animator _animator;
    private int _moveHash;
    private int _runHash;
    private int _aimHash;
    private int _shootHash;
    private bool _shooting = false;

    private WeaponScrObj _currentWeapon;

    private int _layerMask;
    private bool _hit;
    private RaycastHit _hitInfo;
    

    private float _power;
    private float _effectiveRange;
    private AudioSource _audioSource;
    private AudioClip _shotSound;
    private AudioClip _shotEmptySound;
    private AudioClip _reloadSound;

    private ParticleSystem _shotParticleSys;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }


    private void Start()
    {
        _moveHash = Animator.StringToHash("move");
        _runHash = Animator.StringToHash("run");
        _aimHash = Animator.StringToHash("aim");
        _shootHash = Animator.StringToHash("shoot");

        _layerMask = LayerMask.GetMask("Enemy");
    }

    #region Init Actions
    private void OnEnable()
    {
        _inputEventBus.MoveInputEvent += MoveInputEvent;
        _inputEventBus.RunInputEvent += RunAction;
        _inputEventBus.AimInputEvent += AimAction;
        _inputEventBus.ShootInputEvent += ShootAction;

        _playerManagerEventBus.SetWeaponPlaManEvent += SetWeaponAction;
        _playerManagerEventBus.ReloadWeaponPlaManEvent += ReloadWeaponAction;

    }
    private void OnDisable()
    {
        _inputEventBus.MoveInputEvent -= MoveInputEvent;
        _inputEventBus.RunInputEvent -= RunAction;
        _inputEventBus.AimInputEvent -= AimAction;
        _inputEventBus.ShootInputEvent -= ShootAction;

        _playerManagerEventBus.SetWeaponPlaManEvent -= SetWeaponAction;
        _playerManagerEventBus.ReloadWeaponPlaManEvent -= ReloadWeaponAction;
    }
    #endregion


    private void Update()
    {
        ///The aim ray
        Debug.DrawRay(transform.position, transform.forward * _effectiveRange, Color.yellow);
        if (_currentWeapon != null)
        {
            if (_currentWeapon.GetBulletsInMagazine() <= 0 || !_shooting)
            {
                _animator.SetBool(_shootHash, false);
                _animator.speed = 1;
            }
            else
            {
                _hit = Physics.Raycast(transform.position, transform.forward, out _hitInfo, _effectiveRange, _layerMask);
            }
        }
    }

    ///<summary>Activates the move animation when player is moving</summary>
    private void MoveInputEvent(Vector2 direction)
    {
        if (direction.sqrMagnitude > 0)
        {
            _animator.SetBool(_moveHash, true);
            return;
        }
        _animator.SetBool(_moveHash, false);
    }

    ///<summary>Activates the run animation when player is running</summary>
    private void RunAction(bool run)
    {
        _animator.SetBool(_runHash, run);
    }

    ///<summary>Activates the aim animation when player is aiming</summary>
    private void AimAction(bool aim)
    {
        _animator.SetBool(_aimHash, aim);
    }

    ///<summary>Activates when input shoot control is performed/cancelled</summary>
    ///<param name="shoot">True is the input shoot control is performed. False if cancelled</param>
    private void ShootAction(bool shoot)
    {
        if (!_currentWeapon)
        {
            return;
        }
        
        _shooting = shoot;
        
        if (_shooting && _currentWeapon.GetBulletsInMagazine() > 0)
        {            
            _animator.speed = _currentWeapon.GetRateOfFire() / 10f;
            _animator.speed = Mathf.Clamp(_animator.speed, 0.5f, 4f);
            _animator.SetBool(_shootHash, _shooting);
        }
        else if (_shooting && _currentWeapon.GetBulletsInMagazine() <= 0)
        {
            _audioSource.PlayOneShot(_shotEmptySound);
        }
    }

    ///<summary>Sets a new current weapon</summary>
    ///<param name="weapon">The new weapon</param>
    private void SetWeaponAction(Item weapon)
    {
        GameObject weaponPrefab = weapon.GetPrefab();

        if (transform.childCount > 0)
        {
            for (int i=0; i<transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        weaponPrefab = Instantiate(weaponPrefab, transform);
        weaponPrefab.GetComponent<Collider>().enabled = false;

        _currentWeapon = (WeaponScrObj)weapon;
        _power = _currentWeapon.GetPower();
        _effectiveRange = _currentWeapon.GetEffectiveRange();
        _shotSound = _currentWeapon.GetShotSound();
        _shotEmptySound = _currentWeapon.GetShotEmptySound();
        _reloadSound = _currentWeapon.GetReloadSound();

        _shotParticleSys = (ParticleSystem) weaponPrefab.GetComponentInChildren(typeof(ParticleSystem));
        _shotParticleSys.Pause();
        ReportBulletsInMagazine();
    }

    ///<summary>Load current weapon with a number of bullets</summary>
    ///<param name="bullets">The number of bullets to load</param>
    private int ReloadWeaponAction(int bullets)
    {
        int availableBullets = _currentWeapon.GetBulletsInMagazine();
        int freeSpacesInMagazine = _currentWeapon.GetMaxNumberInMagazine() - availableBullets;

        _audioSource.PlayOneShot(_reloadSound);
        if (bullets >= freeSpacesInMagazine)
        {
            availableBullets += freeSpacesInMagazine;
            _currentWeapon.SetBulletsInMagazine(availableBullets);
            ReportBulletsInMagazine();
            return bullets - freeSpacesInMagazine;
        }
        availableBullets += bullets;
        _currentWeapon.SetBulletsInMagazine(availableBullets);

        ReportBulletsInMagazine();
        return 0;
    }

    ///<summary>Activates whith an animation event (shoot animation)</summary>
    private void WeaponShot()
    {
        _audioSource.PlayOneShot(_shotSound);

        _shotParticleSys.Play();                

        int availableBullets = _currentWeapon.GetBulletsInMagazine();

        availableBullets -= 1;

        if (availableBullets < 0)
        {
            availableBullets = 0;
        }
        _currentWeapon.SetBulletsInMagazine(availableBullets);

        if (_hit)
        {
            _playerManagerEventBus.RaiseDamageEnemyPlaManEvent(_hitInfo.collider.gameObject, _power);
        }
        ReportBulletsInMagazine();
    }

    ///<summary>Reports the number of bullets available</summary>
    private void ReportBulletsInMagazine()
    {
        _weaponEventBus.RaiseBulletsInMagazineWeaponEvent(_currentWeapon.GetBulletsInMagazine());
    }


}
