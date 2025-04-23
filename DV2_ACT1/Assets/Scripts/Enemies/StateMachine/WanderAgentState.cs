using System.Collections;
using UnityEngine;
using UnityEngine.AI;

///<summary>Defines the wander state behaviour</summary>
public class WanderAgentState : BaseState
{
    private Vector3 _circleStrengthCenter;
    private Vector3 _circleRateCenter;

    private IEnumerator _steerCoroutine;
    private float _circleRateRadius;


    private void OnEnable()
    {
        _steerCoroutine = ChangeTargetPosition();
        StartCoroutine(_steerCoroutine);
    }
    protected override void Update()
    {
        base.Update();
        _circleRateRadius = _enemySettings.GetCircleStrengthRadius();
        _circleRateCenter = transform.position + transform.forward * _enemySettings.GetDistanceToCSCenter();
    }

    ///<summary>Listens the player detection event</summary>
    ///<param name="detected">True if the player was detected. False if not</param>
    ///<param name="position">The position of the player</param>
    protected override void PlayerDetectedAction(bool detected, Vector3 position)
    {
        base.PlayerDetectedAction(detected, position);
        if (detected)
        {
            ChangeState(GetComponent<PursuitState>());
        }       
    }

    ///<summary>Listens the player listened event</summary>
    ///<param name="listened">True if the player was listened. False if not</param>
    protected override void PlayerListenedAction(bool listened)
    {
        base.PlayerListenedAction(listened);
        if (listened)
        {
            ChangeState(GetComponent<InvestigateState>()); 
        }
    }

    ///<summary>Defines the destination point</summary>
    private IEnumerator ChangeTargetPosition()
    {
        NavMeshHit hit;
        bool hasHit;
        int turnDir = 1;

        while (true && enabled)
        {
            float randomAngle = Random.Range(0f, 360f);
            float x = _circleRateRadius * Mathf.Cos(randomAngle * Mathf.Deg2Rad);
            float z = _circleRateRadius * Mathf.Sin(randomAngle * Mathf.Deg2Rad);

            ///Defines random destination points to simulate a wander
            _circleStrengthCenter = _circleRateCenter + new Vector3(x, 0f, z);
            hasHit = NavMesh.SamplePosition(_circleStrengthCenter, out hit, 3f, NavMesh.AllAreas);

            if (_enemyNavAgent.isOnNavMesh)
            {
                ///Rotates if position out of navmesh area
                if (!hasHit)
                {
                    _enemyNavAgent.destination = transform.position - transform.forward;
                }
                else
                {
                    _enemyNavAgent.destination = _circleStrengthCenter;
                    turnDir = turnDir * -1;
                }
            }
            yield return new WaitForSeconds(_enemySettings.GetTimeToNewPosition());
        }
        yield return null;
    }
}

