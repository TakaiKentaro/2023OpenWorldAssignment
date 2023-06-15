using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IPool
{
    [Header("ステータス")] [SerializeField] private int _health;
    [SerializeField] private int _attack;
    [SerializeField] private int _defence;
    [SerializeField] private int _speed;

    private EnemyStatus _status;

    public bool Waiting { get; set; }

    public void SetUp(Transform parent)
    {
        _status = new EnemyStatus();
        gameObject.SetActive(false);
    }

    public void IsUseSetUp()
    {
        _status.SetStatus(_health, _attack, _defence, _speed);
        gameObject.SetActive(true);
    }

    public bool Execute()
    {
        return _status.Health > 0 ? true : false;
    }

    public void Delete()
    {
        gameObject.SetActive(false);
    }
}