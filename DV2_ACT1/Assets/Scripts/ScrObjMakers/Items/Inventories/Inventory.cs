using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>Controls the inventories</summary>
public abstract class Inventory : ScriptableObject
{    
    public Dictionary<Item, int> _items = new Dictionary<Item, int>();

    ///<summary>Adds an item</summary>
    ///<param name="item">The item to add</param>
    ///<param name="itemIncrement">The quantity of the item to add</param>
    public void AddItem(Item item, int itemIncrement)
    {
        if (!_items.ContainsKey(item))
        {
            _items.Add(item, itemIncrement);
        }
        else
        {
            _items[item] += itemIncrement;
        }
    }

    ///<summary>Removes an specified item</summary>
    ///<param name="item">The specified item</param>
    public void RemoveItem(Item item)
    {
        if (_items.ContainsKey(item))
        {
            _items.Remove(item);
        }
    }

    ///<summary>Decreases the quantity of an specified item</summary>
    ///<param name="item">The specified item</param>
    ///<param name="quantity">The quantity to decrease</param>
    public void DecreaseQuantityOfOneItem(Item item, int quantity)
    {
        if (_items.ContainsKey(item))
        {
            _items[item] -= quantity;
        }
    }

    ///<summary>Return the quantity of the an specified item</summary>
    ///<param name="item">The specified item</param>
    ///<return>The quantity of the specified item</return>
    public int GetQuantityOfOneItem(Item item)
    {
        if (_items.ContainsKey(item))
        {
            return _items[item];
        }
        return 0;
    }

    ///<summary>Returns the number of items</summary>
    ///<return>The number of items</return>
    public int GetNumberOfItems()
    {
        return _items.Count;
    }

    ///<summary>Returns the first item on the inventory</summary>
    ///<return>The first item</return>
    public Item GetFirstItem()
    {
        IDictionaryEnumerator itemsEnum = _items.GetEnumerator();        
        if (itemsEnum.MoveNext())
        {
            return (Item)itemsEnum.Key;
        }
        return null;
    }

    ///<summary>Returns the previous item of the specified item</summary>
    ///<param name="item">The specified item</param>
    ///<return>The previous item of the specified item</return>
    public Item GetPreviousItem(Item item)
    {
        if (_items.Count < 2)
        {
            return item;
        }

        IDictionaryEnumerator itemsEnum = _items.GetEnumerator();
        itemsEnum.MoveNext();
        Item previousItem = (Item)itemsEnum.Key;

        if (previousItem == item)
        {
            return previousItem;
        }

        while (itemsEnum.MoveNext())
        {
            if ((Item)itemsEnum.Key == item)
            {
                return previousItem;
            }
            previousItem = (Item)itemsEnum.Key;
        }
        return null;
    }

    ///<summary>Returns the next item of the specified item</summary>
    ///<param name="item">The specified item</param>
    ///<return>The next item of the specified item</return>
    public Item GetNextItem(Item item)
    {
        if (_items.Count < 2)
        {
            return item;
        }
        IDictionaryEnumerator itemsEnum = _items.GetEnumerator();

        while (itemsEnum.MoveNext())
        {
            if ((Item)itemsEnum.Key == item)
            {
                Item nextItem = (Item)itemsEnum.Key;
                if (itemsEnum.MoveNext())
                {
                    nextItem = (Item)itemsEnum.Key;
                }
                return nextItem;
            }
        }
        return null;
    }

    ///<summary>Returns the item type stored on the inventory</summary>
    ///<return>The item type stored on the inventory</return>
    public abstract Type GetStoredItemType();

}
