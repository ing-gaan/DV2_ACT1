using System;
using UnityEngine;

///<summary>Controls the healers inventory</summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Inventories/Healer inventory", fileName = "New playerHealerInventoryScrObj")]
public class HealerInventoryScrObj : Inventory
{
    ///<summary>Returns the item type stored on the inventory</summary>
    ///<return>The item type stored on the inventory</return>
    public override Type GetStoredItemType()
    {
        return typeof(HealerScrObj);
    }
}
