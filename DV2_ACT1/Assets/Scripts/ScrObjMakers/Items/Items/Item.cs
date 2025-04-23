using UnityEngine;
using UnityEngine.UI;

///<summary>Stores the data of an item</summary>
public abstract class Item : ScriptableObject
{
    [Header("---------- Prefab ----------")]
    [SerializeField] private GameObject _itemPrefab;
    [Header("---------- Specifications ----------")]
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _itemUniqueName;
    [SerializeField] private ItemType _itemType;
    [SerializeField] private int _power;
    [Header("---------- Default values ----------")]
    [SerializeField] private int _defaultIncrement;
    [SerializeField] private int _maxNumberInMagazine;
    [Header("---------- Sounds  ----------")]
    [SerializeField] private AudioClip _collectSound;


    #region Getters
    public GameObject GetPrefab()
    {
        return _itemPrefab;
    }
    public Sprite GetIcon()
    {
        return _icon;
    }
    public string GetItemName()
    {
        return _itemUniqueName;
    }

    public int GetPower()
    {
        return _power;
    }

    public int GetDefaultIncrement()
    {
        return _defaultIncrement;
    }
    
    public int GetMaxNumberInMagazine()
    {
        return _maxNumberInMagazine;
    }

    public AudioClip GetCollecteSound()
    {
        return _collectSound;
    }
    #endregion
    

}
