using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")] [SerializeField] private EnemyController _enemy;
    [SerializeField] private float _spawnInterval = 10f;
    
    private ObjectPool<EnemyController> _enemyPool;
    private PlayerController _playerController;

    private void Start()
    {
        if (_enemy == null)
        {
            return;
        }

        _enemyPool = new ObjectPool<EnemyController>(_enemy, transform);
        _playerController = FindObjectOfType<PlayerController>();
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(_spawnInterval);
        while (true)
        {
            var enemy = _enemyPool.Use();
            enemy.transform.parent = this.transform;

            Vector3 playerPosition = _playerController.transform.position;

            float spawnRadius = 5f;

            Vector3 spawnPosition = playerPosition + Random.insideUnitSphere * spawnRadius;
            spawnPosition.y = playerPosition.y;

            enemy.transform.position = spawnPosition;

            yield return new WaitForSeconds(_spawnInterval);
        }
    }
}