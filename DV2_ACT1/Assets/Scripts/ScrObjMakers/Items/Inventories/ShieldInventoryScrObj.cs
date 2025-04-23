using System;
using UnityEngine;

///<summary>Controls the shields inventory</summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Inventories/Shield inventory", fileName = "New playerShieldInventoryScrObj")]
public class ShieldInventoryScrObj : Inventory
{
    ///<summary>Returns the item type stored on the inventory</summary>
    ///<return>The item type stored on the inventory</return>
    public override Type GetStoredItemType()
    {
        return typeof(ShieldScrObj);
    }
}

