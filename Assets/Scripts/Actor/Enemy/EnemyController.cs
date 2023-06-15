using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour, IPool
{
    [Header("ステータス")] [SerializeField] private int _health;
    [SerializeField] private int _attack;
    [SerializeField] private int _speed;
    [SerializeField] private Renderer[] _rendererChild;
    
    private EnemyStatus _status; 
    private Inventory _inventory;
    private Renderer _renderer;
    public bool Waiting { get; set; }

    public void SetUp(Transform parent)
    {
        _inventory = FindObjectOfType<Inventory>();
        _renderer = GetComponent<Renderer>();

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
        _inventory.AddWallet(_health + _attack + _speed);
        gameObject.SetActive(false);
    }

    public void AddDamage(int dmg)
    {
        _status.Health -= dmg;
    }

    public void RandomStatus(int num)
    {
        _health = 1;
        _attack = 1;
        _speed = 1;

        for (int i = 0; i < num; i++)
        {
            int n = Random.Range(0, 3);

            switch (n)
            {
                case 0:
                    _health++;
                    break;
                case 1:
                    _attack++;
                    break;
                case 2:
                    _speed++;
                    break;
            }
        }

        ChangeColor(_health + _attack + _speed);
        _status.SetStatus(_health, _attack, _speed);
    }

    private void ChangeColor(int num)
    {
        Color currentColor = _renderer.material.color;

        float brightness = 1f - (float)num / 50f;

        Color newColor = new Color(currentColor.r * brightness, currentColor.g * brightness, currentColor.b * brightness);

        for (int i = 0; i < _rendererChild.Length; i++)
        {
            Color childColor = new Color(newColor.r * (1f - i * 0.1f), newColor.g * (1f - i * 0.1f), newColor.b * (1f - i * 0.1f));
            _rendererChild[i].material.color = childColor;
        }

        _renderer.material.color = newColor;
    }
}