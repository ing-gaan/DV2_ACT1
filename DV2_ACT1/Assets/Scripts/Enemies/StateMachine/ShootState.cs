using System.Collections;
using UnityEngine;

///<summary>Defines the shoot state behaviour</summary>
public class ShootState : BaseState
{
    private IEnumerator _shootCoroutine;

    private void OnEnable()
    {
        _shootCoroutine = Shoot();
        StartCoroutine(_shootCoroutine);
        _enemyNavAgent.isStopped = true;
        _animator.SetBool(_attackHash, true);
    }
    protected override void Update()
    {
        base.Update();
        transform.LookAt(_playerPosition);

        if (!_playerDetected || _distanceToPlayer > _enemySettings.GetOutOfRageDistance())
        {
            //Look at the last player position known           
            ChangeState(GetComponent<PursuitState>());
        }
    }

    ///<summary>Change state</summary>
    ///<param name="state">The destination state</param>
    protected override bool ChangeState(BaseState state)
    {
        _playerPosition = _lastPlayerPosition;
        _enemyNavAgent.isStopped = false;
        _animator.SetBool(_attackHash, false);
        _shotParticleSystem.Stop();
        if (!base.ChangeState(state))
        {
            _enemyNavAgent.isStopped = true;
            _animator.SetBool(_attackHash, true);
            _shotParticleSystem.Play();
            return false;
        }
        return true;
    }

    ///<summary>Defines the shoot coroutinet</summary>
    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.11f);
        _shotParticleSystem.Play();
        while (true && enabled)
        {
            //shotParticleSystem.Play();
            _enemyEventBus.RaiseDamagePlayerEnemyEvent(CalculateDamage());
            yield return new WaitForSeconds(1.1f);
            //shotParticleSystem.Stop();
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }

    ///<summary>Calculates the damage done to the player</summary>
    private float CalculateDamage()
    {
        float damage = _distanceToPlayer / _enemySettings.GetOutOfRageDistance();
        damage = 1 - damage;
        damage *= _enemySettings.GetMaxDamage();

        return damage;
    }
}
