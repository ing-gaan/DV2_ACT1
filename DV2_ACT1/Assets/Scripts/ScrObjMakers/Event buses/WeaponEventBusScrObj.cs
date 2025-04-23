using System;
using UnityEngine;


///<summary>Scriptable object to channel the weapon events</summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Event Buses/Weapon events", fileName = "New weaponEventBusScrObj")]
public class WeaponEventBusScrObj : ScriptableObject
{
    #region Action delegates
    public event Action<int> BulletsInMagazineWeaponEvent;
    #endregion

    ///<summary>Raises an event when weapon was shot(one bullet)</summary>
    ///<param name="bulletsInMagazine">The remained bullets</param>
    public void RaiseBulletsInMagazineWeaponEvent(int bulletsInMagazine)
    {
        BulletsInMagazineWeaponEvent?.Invoke(bulletsInMagazine);
    }


}
