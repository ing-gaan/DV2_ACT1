using System;
using UnityEngine;


///<summary>Scriptable object to channel the player manager events</summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Event Buses/Player manager events", fileName = "New playerManagerEventBusScrObj")]
public class PlayerManagerEventBusScrObj : ScriptableObject
{
    #region Func delegates
    public Func<string, Item> GetItemPlaManEvent;
    public Func<int, int> ReloadWeaponPlaManEvent;
    #endregion

    #region Action delegates
    public event Action<Item> SetWeaponPlaManEvent;
    public event Action<GameObject, float> DamageEnemyPlaManEvent;
    public event Action<float, float> UpdatePlayerStatsPlaManEvent;
    public event Action<ShieldScrObj, int> UpdatePlayerShieldStockPlaManEvent;
    public event Action<HealerScrObj, int> UpdatePlayerHealerStockPlaManEvent;
    public event Action PlayerDeadPlaManEvent;
    public event Action PlayerEndsGamePlaManEvent;
    #endregion

    ///<summary>Raises an event when player collects an item</summary>
    ///<param name="itemName">The item name collected</param>
    ///<return>The item collected</return>
    public Item RaiseGetItemPlaManEvent(string itemName)
    {
        if (GetItemPlaManEvent != null)
        {
            return GetItemPlaManEvent(itemName);
        }
        return null;
    }

    ///<summary>Raises an event when player reload a weapon</summary>
    ///<param name="numBullets">The number of bullets to load</param>
    ///<return>The number of bullets really loaded</return>
    public int RaiseReloadWeaponPlaManEvent(int numBullets)
    {
        if (ReloadWeaponPlaManEvent != null)
        {
            return ReloadWeaponPlaManEvent(numBullets);
        }
        return 0;
    }

    ///<summary>Raises an event when player change the weapon</summary>
    ///<param name="weapon">The weapon gameobject</param>
    public void RaiseSetWeaponPlaManEvent(Item weapon)
    {
        SetWeaponPlaManEvent?.Invoke(weapon);
    }

    ///<summary>Raises an event when player damage a enemy</summary>
    ///<param name="enemy">The enemy damaged</param>
    ///<param name="damage">The damage inflicted</param>
    public void RaiseDamageEnemyPlaManEvent(GameObject enemy, float damage)
    {
        DamageEnemyPlaManEvent?.Invoke(enemy, damage);
    }

    ///<summary>Raises an event when player update stats</summary>
    ///<param name="health">The health quantity</param>
    ///<param name="shield">The shield quantity</param>
    public void RaiseUpdatePlayerStatsPlaManEvent(float health, float shield)
    {
        UpdatePlayerStatsPlaManEvent?.Invoke(health, shield);
    }

    ///<summary>Raises an event when player update its shields</summary>
    ///<param name="shield">The current shield</param>
    ///<param name="numShield">The stock of current shield</param>
    public void RaiseUpdatePlayerShieldStockPlaManEvent(ShieldScrObj shield, int numShield)
    {
        UpdatePlayerShieldStockPlaManEvent?.Invoke(shield, numShield);
    }
    ///<summary>Raises an event when player update its healers</summary>
    ///<param name="healer">The current healer</param>
    ///<param name="numHealer">The stock of current healer</param>
    public void RaiseUpdatePlayerHealerStockPlaManEvent(HealerScrObj healer, int numHealer)
    {
        UpdatePlayerHealerStockPlaManEvent?.Invoke(healer, numHealer);
    }

    ///<summary>Raises an event when player is dead</summary>
    public void RaisePlayerDeadPlaManEvent()
    {
        PlayerDeadPlaManEvent?.Invoke();
    }

    ///<summary>Raises an event when player ends the game</summary>
    public void RaisePlayerEndsGamePlaManEvent()
    {
        PlayerEndsGamePlaManEvent?.Invoke();
    }

}
