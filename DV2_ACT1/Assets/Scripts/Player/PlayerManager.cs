using UnityEngine;

///<summary>Controls the player state</summary>
[RequireComponent(typeof(PlayerItemsManager))]
public class PlayerManager : MonoBehaviour
{
    [Header("---------- Event buses ----------")]
    [SerializeField] private InputEventBusScrObj _inputEventBus;
    [SerializeField] private PlayerManagerEventBusScrObj _playerManagerEventBus;
    [SerializeField] private EnemyEventBusScrObj _enemyEventBus;
    [Header("---------- Settings ----------")]
    [SerializeField] private PlayerSettingsScrObj _playerSettings;

    private PlayerItemsManager _playerItems;
    private Inventory _shieldsInventory;
    private Inventory _healersInventory;

    private float _currentHealthLevel;
    private float _currentShieldLevel;

    private ShieldScrObj currentShield;
    private int _currentShieldStock;
    private HealerScrObj _currentHealer;
    private int _currentHealerStock;


    private void OnEnable()
    {
        _inputEventBus.UseHealerInputEvent += UseHealerInputEvent;
        _inputEventBus.ChangeShieldInputEvent += ChangeShieldInputEvent;
        _enemyEventBus.DamagePlayerEnemyEvent += DamagePlayerAction;
    }

    private void OnDisable()
    {
        _inputEventBus.UseHealerInputEvent -= UseHealerInputEvent;
        _inputEventBus.ChangeShieldInputEvent -= ChangeShieldInputEvent;
        _enemyEventBus.DamagePlayerEnemyEvent -= DamagePlayerAction;
    }


    private void Awake()
    {
        _playerItems = GetComponent<PlayerItemsManager>();
    }

    private void Start()
    {
        _currentHealthLevel = _playerSettings.GetMaxHealthLevel();
        _currentShieldLevel = 0;
        UpdatePlayerStats();

        _shieldsInventory = _playerItems.GetInventory(typeof(ShieldInventoryScrObj));
        _healersInventory = _playerItems.GetInventory(typeof(HealerInventoryScrObj));
    }


    ///<summary>Sets a damage quantity to the player</summary>
    ///<param name="damage">The damage quantity</param>
    private void DamagePlayerAction(float damage)
    {
        if (_currentShieldLevel > 0)
        {
            _currentShieldLevel -= damage;
            _currentHealthLevel -= 1;
        }
        else
        {
            _currentHealthLevel -= damage;
        }

        UpdatePlayerStats();

        if (_currentHealthLevel <= 0)
        {
            _playerManagerEventBus.RaisePlayerDeadPlaManEvent();
        }        
    }


    ///<summary>Heals the player if a healer is available</summary>
    private void UseHealerInputEvent()
    {
        if (_healersInventory.GetNumberOfItems() != 0)
        {
            HealPlayer();
        }
        UpdatePlayerStats();
    }

    ///<summary>Changes/uses a shield if there is one available</summary>
    private void ChangeShieldInputEvent()
    {
        if (_shieldsInventory.GetNumberOfItems() != 0)
        {
            ChangeShield();
        }
        UpdatePlayerStats();
    }

    ///<summary>Heals the player with a healer</summary>
    private void HealPlayer()
    {
        _currentHealer = (HealerScrObj) _healersInventory.GetFirstItem();

        _currentHealthLevel += _currentHealer.GetPower();

        if (_currentHealthLevel > _playerSettings.GetMaxHealthLevel())
        {
            _currentHealthLevel = _playerSettings.GetMaxHealthLevel();
        }

        _healersInventory.DecreaseQuantityOfOneItem(_currentHealer, 1);
        _currentHealerStock = _healersInventory.GetQuantityOfOneItem(_currentHealer);
        UpdatePlayerHealerStock();

        if (_currentHealerStock == 0)
        {
            _healersInventory.RemoveItem(_currentHealer);
        }
    }

    ///<summary>Changes/use a shield</summary>
    private void ChangeShield()
    {
        currentShield = (ShieldScrObj)_shieldsInventory.GetFirstItem();

        if (ReduceShieldStock(currentShield))
        {
            UpdatePlayerShieldStock();
            return;
        }
        
        if (_shieldsInventory.GetNumberOfItems() > 1)
        {
            _shieldsInventory.RemoveItem(currentShield);            
            currentShield = (ShieldScrObj)_shieldsInventory.GetFirstItem();
            ReduceShieldStock(currentShield);
            UpdatePlayerShieldStock();
        }
    }

    ///<summary>Reduces the shield inventory</summary>
    private bool ReduceShieldStock(ShieldScrObj shield)
    {
        if (_shieldsInventory.GetQuantityOfOneItem(shield) > 0)
        {
            _currentShieldLevel = _playerSettings.GetMaxShieldLevel();
            _shieldsInventory.DecreaseQuantityOfOneItem(shield, 1);
            return true;
        }
        return false;
    }

    ///<summary>Updates the player stats</summary>
    private void UpdatePlayerStats()
    {
        _playerManagerEventBus.RaiseUpdatePlayerStatsPlaManEvent(_currentHealthLevel, _currentShieldLevel);
    }

    ///<summary>Updates the player's current shield</summary>
    private void UpdatePlayerShieldStock()
    {
        _playerManagerEventBus.RaiseUpdatePlayerShieldStockPlaManEvent(currentShield, _currentShieldStock);
    }

    ///<summary>Updates the player's current healer</summary>
    private void UpdatePlayerHealerStock()
    {
        _playerManagerEventBus.RaiseUpdatePlayerHealerStockPlaManEvent(_currentHealer, _currentHealerStock);
    }


    private void OnTriggerEnter(Collider collider)
    {
        ///Game is finished
        if (collider.name == "EndGameCollider")
        {
            _playerManagerEventBus.RaisePlayerEndsGamePlaManEvent();
        }
    }


}
