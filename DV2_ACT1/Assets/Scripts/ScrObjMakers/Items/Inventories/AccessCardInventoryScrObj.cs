using System;
using UnityEngine;

///<summary>Controls the security cards inventory</summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Inventories/Access card inventory", fileName = "New playerAccesCardInventoryScrObj")]
public class AccessCardInventoryScrObj : Inventory
{
    ///<summary>Returns the item type stored on the inventory</summary>
    ///<return>The item type stored on the inventory</return>
    public override Type GetStoredItemType()
    {
        return typeof(AccessCardScrObj);
    }

}
