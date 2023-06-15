using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")] [SerializeField] private EnemyController _enemy;
    [SerializeField] private float _spawnInterval = 10f;
    [SerializeField] private float _spawnRadius = 10f;

    private ObjectPool<EnemyController> _enemyPool;
    private PlayerController _playerController;
    public int _count;

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
            enemy.RandomStatus(_count);
            enemy.transform.parent = this.transform;

            Vector3 playerPosition = _playerController.transform.position;

            Vector2 randomCircle = Random.insideUnitCircle * _spawnRadius;
            Vector3 spawnPosition = new Vector3(playerPosition.x + randomCircle.x, playerPosition.y,
                playerPosition.z + randomCircle.y);
            spawnPosition.y = playerPosition.y;

            enemy.transform.position = spawnPosition;

            yield return new WaitForSeconds(_spawnInterval);
        }
    }
}