using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHitDetection : MonoBehaviour
{
    [SerializeField] private float _knockbackForce = 10f;
    [SerializeField] private float _knockbackDuration = 0.5f;
    [SerializeField] private bool _isGolden = false;
    private NavMeshAgent _agent;
    private float _speed;
    private EnemyController _enemy;
    private GoldenEnemyController _goldenEnemy;
    private PlayerController _player;

    private void Start()
    {
        if (_isGolden)
        {
            _goldenEnemy = GetComponent<GoldenEnemyController>();
        }
        else
        {
            _enemy = GetComponent<EnemyController>();
        }

        _player = GameObject.FindObjectOfType<PlayerController>();
        _agent = GetComponent<NavMeshAgent>();
        _speed = _agent.speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            Vector3 knockbackDirection = -transform.forward;
            _agent.velocity = knockbackDirection * _knockbackForce;
            if (_isGolden)
            {
                _goldenEnemy.AddDamage(_player._status.Attack);
            }
            else
            {
                _enemy.AddDamage(_player._status.Attack);
            }

            if (gameObject.activeSelf)
            {
                StartCoroutine(ResetVelocity());
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            Vector3 knockbackDirection = -transform.forward;
            _agent.velocity = knockbackDirection * _knockbackForce;
            StartCoroutine(ResetVelocity());
        }
    }

    private IEnumerator ResetVelocity()
    {
        yield return new WaitForSeconds(_knockbackDuration);

        if (gameObject.activeSelf)
        {
            _agent.velocity = Vector3.zero;
            _agent.speed = _speed;
        }
    }
}