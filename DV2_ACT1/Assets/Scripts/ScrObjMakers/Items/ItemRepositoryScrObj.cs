using System.Collections.Generic;
using UnityEngine;

///<summary>Stores the all items created</summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Item repository", fileName = "New itemRepositoryScrObj")]
public class ItemRepositoryScrObj : ScriptableObject
{
    [Header("---------- Healer list ----------")]
    [SerializeField] private List<Item> _healers;
    [Header("---------- Shield list ----------")]
    [SerializeField] private List<Item> _shields;
    [Header("---------- Weapon list ----------")]
    [SerializeField] private List<Item> _weapons;
    [Header("---------- Access card list ----------")]
    [SerializeField] private List<Item> _accessCards;

    private Dictionary<string, Item> _items = new Dictionary<string, Item>();

    private void OnEnable()
    {
        AddListToDictionary(_healers);
        AddListToDictionary(_shields);
        AddListToDictionary(_weapons);
        AddListToDictionary(_accessCards);
    }

    ///<summary>Adds a list of items</summary>
    ///<param name="list">The list of item to add</param>
    private void AddListToDictionary(List<Item> list)
    {
        for (int i=0; i<list.Count; i++)
        {
            _items.Add(list[i].GetItemName().ToLower(), list[i]);
        }
    }

    ///<summary>Searches an item by name on the repository</summary>
    ///<param name="itemUniqueName">The item's name</param>
    ///<return>The item with that name</return>
    public Item GetItem(string itemUniqueName)
    {
        foreach (string name in _items.Keys)
        {
            if (itemUniqueName.ToLower().Contains(name))
            {
                return _items[name];
            }
        }
        return null;
    }
}
