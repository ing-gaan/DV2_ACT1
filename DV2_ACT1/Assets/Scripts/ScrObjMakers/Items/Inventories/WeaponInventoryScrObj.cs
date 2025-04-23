using System;
using UnityEngine;

///<summary>Controls the weapons inventory</summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Inventories/Weapon inventory", fileName = "New playerWeaponInventoryScrObj")]
public class WeaponInventoryScrObj : Inventory
{
    ///<summary>Returns the item type stored on the inventory</summary>
    ///<return>The item type stored on the inventory</return>
    public override Type GetStoredItemType()
    {
        return typeof(WeaponScrObj);
    }
}
