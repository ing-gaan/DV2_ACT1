using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

///<summary>Controls the general aspects of the game</summary>
public class GameManager : MonoBehaviour
{
    [Header("---------- Event buses ----------")]
    [SerializeField] private PlayerManagerEventBusScrObj _playerManagerEventBus;
    [SerializeField] private WeaponEventBusScrObj _weaponEventBus;
    [SerializeField] private PostMessageEventBusScrObj _postMessageEventBus;
    [Header("---------- State bars ----------")]
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private ShieldBar _shieldBar;
    [Header("---------- Icons ----------")]
    [SerializeField] private Image _healthBarIcon;
    [SerializeField] private Image _shieldBarIcon;
    [SerializeField] private Image _weaponIcon;
    [SerializeField] private Image _shieldIcon;
    [SerializeField] private Image _healerIcon;
    [Header("---------- Texts ----------")]
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private TextMeshProUGUI _numberBullets;
    [SerializeField] private TextMeshProUGUI _numberShields;
    [SerializeField] private TextMeshProUGUI _numberHealers;            
    [Header("---------- Others ----------")]
    [SerializeField] private ItemRepositoryScrObj _itemRepositoryScrObj;
    [SerializeField] private PlayerSettingsScrObj _playerSettings;

    private IEnumerator _messageCoroutine; 

    private void OnEnable()
    {
        _playerManagerEventBus.GetItemPlaManEvent += GetItemPlaManEvent;      
        _playerManagerEventBus.SetWeaponPlaManEvent += SetWeaponPlaManEvent;
        _playerManagerEventBus.UpdatePlayerStatsPlaManEvent += UpdatePlayerStatsPlaManEvent;
        _playerManagerEventBus.UpdatePlayerShieldStockPlaManEvent += UpdatePlayerShieldStockPlaManEvent;
        _playerManagerEventBus.UpdatePlayerHealerStockPlaManEvent += UpdatePlayerHealerStockPlaManEvent;
        _playerManagerEventBus.PlayerDeadPlaManEvent += PlayerDeadPlaManEvent;
        _playerManagerEventBus.PlayerEndsGamePlaManEvent += PlayerEndsGamePlaManEvent;
        _weaponEventBus.BulletsInMagazineWeaponEvent += BulletsInMagazineWeaponEvent;
        _postMessageEventBus.PostMessageEvent += PostMessageEvent;
    }
    private void OnDisable()
    {
        _playerManagerEventBus.GetItemPlaManEvent -= GetItemPlaManEvent;
        _playerManagerEventBus.SetWeaponPlaManEvent -= SetWeaponPlaManEvent;
        _playerManagerEventBus.UpdatePlayerStatsPlaManEvent -= UpdatePlayerStatsPlaManEvent;
        _playerManagerEventBus.UpdatePlayerShieldStockPlaManEvent -= UpdatePlayerShieldStockPlaManEvent;
        _playerManagerEventBus.UpdatePlayerHealerStockPlaManEvent -= UpdatePlayerHealerStockPlaManEvent;
        _playerManagerEventBus.PlayerDeadPlaManEvent -= PlayerDeadPlaManEvent;
        _playerManagerEventBus.PlayerEndsGamePlaManEvent -= PlayerEndsGamePlaManEvent;
        _weaponEventBus.BulletsInMagazineWeaponEvent -= BulletsInMagazineWeaponEvent;
        _postMessageEventBus.PostMessageEvent -= PostMessageEvent;
    }

    private void Awake()
    {
        SceneManager.LoadSceneAsync("Level_1", LoadSceneMode.Additive);
    }

    private void Start()
    {
        _healthBar.SetMaxLevelBar(_playerSettings.GetMaxHealthLevel());
        _shieldBar.SetMaxLevelBar(_playerSettings.GetMaxShieldLevel());
        _shieldBar.SetBarLevel(0f);
    }

    ///<summary>Searches an item by name into the items repository</summary>
    ///<param name="itemName">The item's name</param>
    ///<return>The item with that name or null</return>
    private Item GetItemPlaManEvent(string itemName)
    {        
        return _itemRepositoryScrObj.GetItem(itemName);
    }

    ///<summary>Prints the weapon icon on the screen</summary>
    ///<param name="weapon">The weapon</param>
    private void SetWeaponPlaManEvent(Item weapon)
    {
        WeaponScrObj currentWeapon = (WeaponScrObj) weapon;
        _weaponIcon.sprite = currentWeapon.GetIcon();        
    }

    ///<summary>Prints the available bullets</summary>
    ///<param name="numBullets">The number of bullets</param>
    private void BulletsInMagazineWeaponEvent(int numBullets)
    {
        _numberBullets.text = numBullets.ToString();
    }

    ///<summary>Updates the player's stats bars</summary>
    ///<param name="health">The quantity of available health</param>
    ///<param name="shield">The quantity of available shield</param>
    private void UpdatePlayerStatsPlaManEvent(float health, float shield)
    {
        _healthBar.SetBarLevel(health);
        _shieldBar.SetBarLevel(shield);
    }

    ///<summary>Updates the player's available shields</summary>
    ///<param name="shield">The current shield</param>
    ///<param name="qtyShield">The number of current shield</param>
    private void UpdatePlayerShieldStockPlaManEvent(ShieldScrObj shield, int qtyShield)
    {
        _shieldBarIcon.sprite = shield.GetIcon();
        _shieldIcon.sprite = shield.GetIcon();
        _numberShields.text = qtyShield.ToString();
    }

    ///<summary>Updates the player's available healers</summary>
    ///<param name="healer">The current healer</param>
    ///<param name="qtyHealer">The number of current healer</param>
    private void UpdatePlayerHealerStockPlaManEvent(HealerScrObj healer, int qtyHealer)
    {
        _healthBarIcon.sprite = healer.GetIcon();
        _healerIcon.sprite = healer.GetIcon();
        _numberHealers.text = qtyHealer.ToString();
    }

    ///<summary>It activates when player is dead</summary>
    private void PlayerDeadPlaManEvent()
    {
        SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
    }

    ///<summary>It activates when player arrive to the end</summary>
    private void PlayerEndsGamePlaManEvent()
    {
        SceneManager.LoadScene("EndGame", LoadSceneMode.Single);
    }

    ///<summary>Prints a message on the screen</summary>
    ///<param name="message">The message to print</param>
    private void PostMessageEvent(string message)
    {
        _messageCoroutine = MessageDelay(message);
        StartCoroutine(_messageCoroutine);
    }

    ///<summary>Keep a message on screen certain time</summary>
    ///<param name="message">The message to keep on screen</param>
    private IEnumerator MessageDelay(string message)
    {
        _infoText.text = message;
        yield return new WaitForSeconds(5f);
        _infoText.text = "";
    }




}
