using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("ステータス")] [SerializeField] private int _health;
    [SerializeField] private int _attack;
    [SerializeField] private int _speed;

    public PlayerStatus _status;
    private PlayerMove _move;

    private void Start()
    {
        _move = GetComponent<PlayerMove>();
        _status = new PlayerStatus();
        SetStatus(_health, _attack, _speed);
    }

    private void SetStatus(int hp, int atk, int spd)
    {
        _move.AddMoveSpeed(spd);
        _status.SetStatus(_health, _attack, _speed);
    }

    public void UpdateStatus(int hp, int atk, int spd)
    {
        _health += hp;
        _attack += atk;
        _speed += spd;
        SetStatus(_health, _attack, _speed);
    }
}