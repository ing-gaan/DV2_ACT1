using System;
using UnityEngine;


///<summary>Scriptable object to channel the AgentDetector events</summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Event Buses/Enemy events", fileName = "New enemyEventBusScrObj")]
public class EnemyEventBusScrObj : ScriptableObject
{
    #region Public Actions
    public event Action<float> DamagePlayerEnemyEvent;
    #endregion


    ///<summary>Raises an event when the player has been damage</summary>
    ///<param name="damage">The quantity of damage</param>
    public void RaiseDamagePlayerEnemyEvent(float damage)
    {
        DamagePlayerEnemyEvent?.Invoke(damage);
    }
}
