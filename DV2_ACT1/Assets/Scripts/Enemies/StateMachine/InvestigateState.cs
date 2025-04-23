using System.Collections;
using UnityEngine;

///<summary>Defines the investigate state behaviour</summary>
public class InvestigateState : BaseState
{
    private IEnumerator _rotateCoroutine;
    private void OnEnable()
    {
        _rotateCoroutine = Rotate();
        StartCoroutine(_rotateCoroutine);
        _enemyNavAgent.isStopped = true;
    }

    protected override void Update()
    {
        base.Update();

        if (_playerDetected)
        {
            StopCoroutine(_rotateCoroutine);
            _lastPlayerPosition = _playerPosition;
            ChangeState(GetComponent<PursuitState>());            
        }        
    }

    ///<summary>Rotate the enemy</summary>
    private IEnumerator Rotate()
    {
        _animator.SetBool(_rotateHash, true);
        float angle = 0f;
        while (angle < 360 && enabled)
        {
            transform.Rotate(Vector3.up, 1f);            
            angle++;
            yield return new WaitForSeconds(0.01f);
        }

        if (!_playerDetected)
        {         
            ChangeState(GetComponent<WanderAgentState>());
        }
        yield return null;
    }

    ///<summary>Change state</summary>
    ///<param name="state">The destination state</param>
    protected override bool ChangeState(BaseState state)
    {
        _enemyNavAgent.isStopped = false;
        _animator.SetBool(_rotateHash, false);
        if (!base.ChangeState(state))
        {
            _enemyNavAgent.isStopped = true;
            _animator.SetBool(_rotateHash, true);
            return false;
        }
        return true;
    }
}
