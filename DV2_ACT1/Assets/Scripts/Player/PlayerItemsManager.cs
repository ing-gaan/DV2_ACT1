using System;
using System.Collections.Generic;
using UnityEngine;

///<summary>Controls the player's items</summary>
public class PlayerItemsManager : MonoBehaviour
{
    [Header("---------- Event buses ----------")]
    [SerializeField] private PlayerManagerEventBusScrObj _playerManagerEventBus;
    [SerializeField] private PostMessageEventBusScrObj _postMessageEventBus;
    [Header("---------- Settings ----------")]
    [SerializeField] private PlayerSettingsScrObj _playerSettings;
    [Header("---------- Inventories ----------")]
    [SerializeField] private List<Inventory> _inventories;
    [Header("---------- Messages ----------")]
    [SerializeField] private ScreenMessagesScrObj _screenMessages;


    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        //Add default weapon
        Item item = _playerManagerEventBus.RaiseGetItemPlaManEvent(_playerSettings.GetDefaultWeapon());
        AddItemToInventory(item);
    }

    private void OnTriggerEnter(Collider collider)
    {
        ///Collects an item
        if (collider.tag == "Item")
        {
            Destroy(collider.gameObject);
            Item item = _playerManagerEventBus.RaiseGetItemPlaManEvent(collider.name);
            AddItemToInventory(item);
            audioSource.PlayOneShot(item.GetCollecteSound());
            PostMessage(item);
        }
    }

    ///<summary>Adds the collected item to the inventory</summary>
    ///<param name="item">The item to add</param>
    private void AddItemToInventory(Item item)
    {        
        for (int i = 0; i < _inventories.Count; i++)
        {
            if (item.GetType() == _inventories[i].GetStoredItemType())
            {
                _inventories[i].AddItem(item, item.GetDefaultIncrement());
                if(item.GetType() == typeof(ShieldScrObj))
                {
                    _playerManagerEventBus.RaiseUpdatePlayerShieldStockPlaManEvent((ShieldScrObj)item, item.GetDefaultIncrement());
                }
                else if(item.GetType() == typeof(HealerScrObj))
                {
                    _playerManagerEventBus.RaiseUpdatePlayerHealerStockPlaManEvent((HealerScrObj)item, item.GetDefaultIncrement());
                }
                    break;
            }
        }
    }

    ///<summary>Returns a player's inventory</summary>
    ///<param name="inventoryType">The inventory's type to return</param>
    public Inventory GetInventory(Type inventoryType)
    {
        return _inventories.Find(inventory => inventory.GetType() == inventoryType);
    }

    ///<summary>Prints a message on screen related to an item</summary>
    ///<param name="item">The item related</param>
    public void PostMessage(Item item)
    {       
        if (item.GetType() == typeof(AccessCardScrObj))
        {
            AccessCardScrObj card = (AccessCardScrObj)item;
            string message = $"{_screenMessages._accessCardTaken} {card.GetSecurityLevel()}";
            _postMessageEventBus.RaisePostMessageEvent(message);
        }
    }


}
