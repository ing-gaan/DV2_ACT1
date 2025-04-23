using UnityEngine;

///<summary>Controls the player's access security level</summary>
public class PlayerAccesSecurityManager : MonoBehaviour
{
    private PlayerItemsManager _playerItems;

    private void Start()
    {
        _playerItems = GetComponent<PlayerItemsManager>();
    }

    ///<summary>Retunrs the max player's security permission</summary>
    ///<return>The permission level</return>
    public int GetMaxPermissionLevel()
    {
        int maxPermisionLevel = 0;
        Inventory accessCardInventory = _playerItems.GetInventory(typeof(AccessCardInventoryScrObj));
        maxPermisionLevel = CheckAccessCardsSecurityLevel(accessCardInventory);

        return maxPermisionLevel;
    }

    ///<summary>Checks the max security level of the cards in the inventory</summary>
    ///<return>The max security level</return>
    private int CheckAccessCardsSecurityLevel(Inventory inventory)
    {
        int currentValue;
        int maxValue = 0;

        AccessCardScrObj currentItem = (AccessCardScrObj)inventory.GetFirstItem();

        if (currentItem == null)
        {
            return 0;
        }

        AccessCardScrObj nextItem = (AccessCardScrObj)inventory.GetNextItem(currentItem);

        if (currentItem == nextItem)
        {
            return currentItem.GetSecurityLevel();
        }

        maxValue = currentItem.GetSecurityLevel();     
        while (currentItem != nextItem)
        {
            currentValue = nextItem.GetSecurityLevel();
            if (currentValue > maxValue)
            {
                maxValue = currentValue;   
            }
            currentItem = nextItem;
            nextItem = (AccessCardScrObj)inventory.GetNextItem(nextItem);
        }

        return maxValue;
    }
}
