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

    [SerializeField] private ClearManager _clearManager;
    private EnemyStatus _status; 
    private void Start()
    {
        _status = new EnemyStatus();
        _status.SetStatus(_health,_attack,_speed);
    }

    public void AddDamage(int dmg)
    {
        _status.Health -= dmg;

        if (_status.Health <= 0)
        {
            _clearManager.CountUp();
            gameObject.SetActive(false);
        }
    }
}
