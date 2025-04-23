using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

///<summary>Controls the damage done by the player</summary>
public class Damage : MonoBehaviour
{
    [Header("---------- Event buses ----------")]
    [SerializeField] private PlayerManagerEventBusScrObj _playerManagerEventBus;
    [Header("---------- Settings ----------")]
    [SerializeField] private EnemySettingsScrObj _enemySettings;
    [SerializeField] private GameObject _replacementGameObj;

    private IEnumerator _deathCoroutine;
    private Animator _animator;
    private NavMeshAgent _enemyNavMesh;
    private ParticleSystem _shotParticleSystem;

    private float _currentHealth;
    private int _deathHash;
    private bool _dead = false;

    #region Init Actions
    private void OnEnable()
    {
        _playerManagerEventBus.DamageEnemyPlaManEvent += DamageEnemyPlaManEvent;
    }
    private void OnDisable()
    {
        _playerManagerEventBus.DamageEnemyPlaManEvent -= DamageEnemyPlaManEvent;
    }
    #endregion

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemyNavMesh = GetComponent<NavMeshAgent>();
        _shotParticleSystem = GetComponentInChildren<ParticleSystem>();       
        _deathHash = Animator.StringToHash("death");
    }

    private void Start()
    {               
        _currentHealth = _enemySettings.GetMaxHealth();
    }

    ///<summary>Listens when player attacks</summary>
    ///<param name="enemy">The enemy who recives the attack</param>
    ///<param name="damage">The quantity of damage</param>
    private void DamageEnemyPlaManEvent(GameObject enemy, float damage)
    {
        if (gameObject == enemy)
        {            
            _currentHealth -= damage;            
            if (_currentHealth <= 0 && !_dead)
            {
                _deathCoroutine = Death();
                StartCoroutine(_deathCoroutine);
            }
        }
    }

    ///<summary>Enemies dead procedure</summary>
    private IEnumerator Death()
    {
        _dead = true;
        _enemyNavMesh.isStopped = true;
        _animator.SetBool(_deathHash, true);
        _shotParticleSystem.Stop();
        yield return new WaitForSeconds(5f);

        if (_replacementGameObj != null)
        {
            Instantiate(_replacementGameObj, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
        yield return null;
    }

}
