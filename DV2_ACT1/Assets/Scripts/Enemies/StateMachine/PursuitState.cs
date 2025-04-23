using UnityEngine;

///<summary>Defines the pursuit state behaviour</summary>
public class PursuitState : BaseState
{
    protected override void Update()
    {
        base.Update();

        DefineDestination();

        if (_playerDetected && _distanceToPlayer < _enemySettings.GetMinDistanceToShoot())
        {
            ChangeState(GetComponent<ShootState>());
        }
        else if (_playerListened && !_playerDetected)
        {
            ChangeState(GetComponent<InvestigateState>());
        }
        else if (Vector3.Distance(transform.position, _lastPlayerPosition) < 1f)
        {
            ChangeState(GetComponent<WanderAgentState>());
        }
        
    }

    ///<summary>Sets the enemy destination</summary>
    private void DefineDestination()
    {
        if (_playerDetected)
        {
            _enemyNavAgent.destination = _playerPosition;
        }
        else
        {
            _enemyNavAgent.destination = _lastPlayerPosition;
        }
    }
}
