using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour, IPool
{
    [Header("ステータス")] [SerializeField] private int _health;
    [SerializeField] private int _attack;
    [SerializeField] private int _defence;
    [SerializeField] private int _speed;

    private EnemyStatus _status;
    private Inventory _inventory;

    public bool Waiting { get; set; }

    public void SetUp(Transform parent)
    {
        _inventory = FindObjectOfType<Inventory>();

        _status = new EnemyStatus();
        gameObject.SetActive(false);
    }

    public void IsUseSetUp()
    {
        _status.SetStatus(_health, _attack, _speed);
        gameObject.SetActive(true);
    }

    public bool Execute()
    {
        return _status.Health > 0 ? true : false;
    }

    public void Delete()
    {
        _inventory.AddWallet(_health);
        gameObject.SetActive(false);
    }

    public void AddDamage(int dmg)
    {
        _status.Health -= dmg;
    }
}