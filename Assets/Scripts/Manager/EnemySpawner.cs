using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")] [SerializeField] private EnemyController _enemy;

    [Header("スポーン位置")] [SerializeField] private Vector3 _spawnPos;

    [Header("ランダム位置範囲")] [SerializeField] private Vector3 _minOffset;
    [SerializeField] private Vector3 _maxOffset;

    private ObjectPool<EnemyController> _enemyPool;

    private void Start()
    {
        if (_enemy == null || _spawnPos == null)
        {
            return;
        }
    }

    private void Update()
    {
        IPool enemy = _enemyPool.Use();
        _enemy.transform.parent = this.transform;
        _enemy.transform.position = GetRandomPosition(_spawnPos, _minOffset, _maxOffset);
    }

    private Vector3 GetRandomPosition(Vector3 center, Vector3 minOffset, Vector3 maxOffset)
    {
        Vector3 randomOffset = new Vector3(UnityEngine.Random.Range(minOffset.x, maxOffset.x),
            center.y,
            UnityEngine.Random.Range(minOffset.z, maxOffset.z));
        return center + randomOffset;
    }
}