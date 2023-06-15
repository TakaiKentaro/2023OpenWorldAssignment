using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenEnemyController : MonoBehaviour
{
    [Header("ステータス")] 
    [SerializeField] private int _health;
    [SerializeField] private int _attack;
    [SerializeField] private int _speed;
    [SerializeField] private int _number;
    private EnemyStatus _status;
    private ClearManager _clearManager;
    private void Start()
    {
        _clearManager = FindObjectOfType<ClearManager>();
        _status = new EnemyStatus();
        _status.SetStatus(_health,_attack,_speed);
    }

    public void AddDamage(int dmg)
    {
        _status.Health -= dmg;

        if (_status.Health <= 0)
        {
            _clearManager.CountUp(_number);
            gameObject.SetActive(false);
        }
    }
}
