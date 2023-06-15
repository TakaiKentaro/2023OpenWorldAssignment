using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")] [SerializeField] private EnemyController _enemy;
    [SerializeField] private float _spawnInterval = 10f;
    
    
    [NonSerialized]public List<Vector3> _spawnPosList = new List<Vector3>();

    private ObjectPool<EnemyController> _enemyPool;

    private void Start()
    {
        if (_enemy == null)
        {
            return;
        }

        _enemyPool = new ObjectPool<EnemyController>(_enemy, transform);

        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            var enemy = _enemyPool.Use();
            enemy.transform.parent = this.transform;
            _enemy.transform.position = _spawnPosList[Random.Range(0, _spawnPosList.Count)];
            yield return new WaitForSeconds(_spawnInterval);
        }
    }
}