using UnityEngine;

///<summary>Controls the player's weapons</summary>
public class PlayerWeaponManager: MonoBehaviour
{
    [Header("---------- Event buses ----------")]
    [SerializeField] private InputEventBusScrObj _inputEventBus;
    [SerializeField] private PlayerManagerEventBusScrObj _playerManagerEventBus;

    private PlayerItemsManager _playerItems;

    private Inventory _weaponsInventory;
    private Item _currentWeapon;



    private void OnEnable()
    {
        _inputEventBus.ChangeWeaponInputEvent += ChangeWeaponInputEvent;
        _inputEventBus.ReloadWeaponInputEvent += ReloadWeaponInputEvent;
    }

    private void OnDisable()
    {
        _inputEventBus.ChangeWeaponInputEvent -= ChangeWeaponInputEvent;
        _inputEventBus.ReloadWeaponInputEvent -= ReloadWeaponInputEvent;
    }


    private void Awake()
    {
        _playerItems = GetComponent<PlayerItemsManager>();
    }

    private void Start()
    {
        ///Searches the weapon inventory
        _weaponsInventory = _playerItems.GetInventory(typeof(WeaponInventoryScrObj));
    }

    ///<summary>Changes the current weapon when the mouse scroll control is performed</summary>
    ///<param name="scroll">The number scrolling actions performed</param>
    private void ChangeWeaponInputEvent(int scroll)
    {
        if (_currentWeapon == null)
        {
            _currentWeapon = _weaponsInventory.GetFirstItem();            
        }
        if (scroll == 1)
        {
            SetWeapon(_weaponsInventory.GetNextItem(_currentWeapon));            
        }
        else if (scroll == -1)
        {
            SetWeapon(_weaponsInventory.GetPreviousItem(_currentWeapon));
        }

        _playerManagerEventBus.RaiseSetWeaponPlaManEvent(_currentWeapon);
        if (((WeaponScrObj)_currentWeapon).GetBulletsInMagazine() == 0)
        {
            LoadWeapon();
        }
    }

    ///<summary>Reloads the current weapon when input reload control is performed</summary>
    private void ReloadWeaponInputEvent()
    {
        LoadWeapon();
    }

    ///<summary>Sets a new current weapon</summary>
    ///<param name="weapon">The new weapon</param>
    private void SetWeapon(Item weapon)
    {
        _playerManagerEventBus.RaiseSetWeaponPlaManEvent(weapon);
        _currentWeapon = weapon;
    }

    ///<summary>Load current weapon with a number of bullets</summary>
    private void LoadWeapon()
    {
        if (_weaponsInventory.GetQuantityOfOneItem(_currentWeapon) == 0)
        {
            return;
        }
        int bulletsToLoad;
        if (_weaponsInventory.GetQuantityOfOneItem(_currentWeapon) < _currentWeapon.GetDefaultIncrement())
        {
            bulletsToLoad = _weaponsInventory.GetQuantityOfOneItem(_currentWeapon);
        }
        else
        {
            bulletsToLoad = _currentWeapon.GetDefaultIncrement();
        }
        int leftoverBullets = _playerManagerEventBus.RaiseReloadWeaponPlaManEvent(bulletsToLoad);

        _weaponsInventory.DecreaseQuantityOfOneItem(_currentWeapon, bulletsToLoad - leftoverBullets);
    }

}
