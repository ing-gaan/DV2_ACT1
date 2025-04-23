using UnityEngine;

///<summary>Scriptable object to store the player settings</summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Settings/Player settings", fileName = "New playerSettingsScrObj")]
public class PlayerSettingsScrObj : ScriptableObject
{
    [Header("---------- Player settings ----------")]
    [SerializeField] private float _walkSpeed = 10f;
    [SerializeField] private float _runSpeed = 20f;
    [SerializeField] private float _jumpSpeed = 5f;
    [SerializeField] private float _maxHealthLevel;
    [SerializeField] private float _maxShieldLevel;
    [SerializeField] private string _defaultWeapon = "Gun";

    #region Getters
    public float GetWalkSpeed()
    {
        return _walkSpeed;
    }
    public float GetRunSpeed()
    {
        return _runSpeed;
    }
    public float GetJumpSpeed()
    {
        return _jumpSpeed;
    }
    public float GetMaxHealthLevel()
    {
        return _maxHealthLevel;
    }
    public float GetMaxShieldLevel()
    {
        return _maxShieldLevel;
    }
    public string GetDefaultWeapon()
    {
        return _defaultWeapon;
    }
    #endregion
}
