using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")] [SerializeField] private EnemyController _enemy;

    [Header("スポーン位置高さ")] [SerializeField] private float _spawnPosY;

    [Header("ランダム位置範囲")] [SerializeField] private Vector3 _minOffset;
    [SerializeField] private Vector3 _maxOffset;

    [SerializeField] private float _spawnInterval = 10f;

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
            _enemy.transform.position = new Vector3(
                Random.Range(_minOffset.x, _maxOffset.x),
                _spawnPosY,
                Random.Range(_minOffset.z, _maxOffset.z)
            );
            yield return new WaitForSeconds(_spawnInterval);
        }
    }
}