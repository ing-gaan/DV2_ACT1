using UnityEngine;

///<summary>Scriptable object to store the enemies settings</summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Settings/Enemy settings", fileName = "New enemySettingsScrObj")]
public class EnemySettingsScrObj : ScriptableObject
{
    [Header("---------- Wander settings ----------")]
    [SerializeField] private float _timeToNewPosition;
    [SerializeField] private float _distanceToCircleStrengthCenter;
    [SerializeField] private float _circleStrengthRadius;
    [Header("---------- FOV settings ----------")]
    [SerializeField] private float _maxFOVDistance;
    [SerializeField] private float _FOVAngle;
    [Header("---------- Another settings ----------")]
    [SerializeField] private float _minDistanceToShoot;
    [SerializeField] private float _outOfRageDistance;
    [SerializeField] private float _listenDistance;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _maxDamage;



    #region Getters
    public float GetTimeToNewPosition()
    {
        return _timeToNewPosition;
    }
    public float GetDistanceToCSCenter()
    {
        return _distanceToCircleStrengthCenter;
    }
    public float GetCircleStrengthRadius()
    {
        return _circleStrengthRadius;
    }
    public float GetFOVDistance()
    {
        return _maxFOVDistance;
    }
    public float GetFOVAngle()
    {
        return _FOVAngle;
    }
    public float GetMinDistanceToShoot()
    {
        return _minDistanceToShoot;
    }
    public float GetOutOfRageDistance()
    {
        return _outOfRageDistance;
    }
    public float GetListenDistance()
    {
        return _listenDistance;
    }
    public float GetMaxHealth()
    {
        return _maxHealth;
    }
    public float GetMaxDamage()
    {
        return _maxDamage;
    }
    #endregion

}
