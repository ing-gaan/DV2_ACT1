using UnityEngine;

///<summary>Stores the data of an access card</summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Items/AccessCard", fileName = "New accessCardScrObj")]
public class AccessCardScrObj : Item
{
    [Header("---------- Access Level ----------")]
    [Range(1, 3)]
    [SerializeField] private int _securityAccessLevel = 1;

    public int GetSecurityLevel()
    {
        return _securityAccessLevel;
    }
}
