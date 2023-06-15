using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _wallet = 0;
    [SerializeField] private Text _walletText;

    [SerializeField] private Text _hpText;
    [SerializeField] private Text _atkText;
    [SerializeField] private Text _spdText;

    public int Wallet
    {
        get => _wallet;
        set => _wallet = value;
    }

    [SerializeField] private GameObject _inventoryPanel;

    [SerializeField] private GameObject _buttonPrefab;

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private EnemySpawner _enemySpawner;
    private void Start()
    {
        _walletText.text = _wallet.ToString();
        OnStopTime(false);
    }

    public void GetItem(ItemData item)
    {
        var obj = Instantiate(_buttonPrefab);
        obj.transform.SetParent(_inventoryPanel.transform, false);
        obj.name = $"Button({item.Name})";

        var button = obj.GetComponent<Button>();
        button.onClick.AddListener(() => UseItem(item));
        button.onClick.AddListener(() => Destroy(obj));

        var text = obj.GetComponentInChildren<Text>();
        text.text = item.Name;

        if (item.Sprite != null)
        {
            obj.GetComponent<Image>().sprite = item.Sprite;
        }

        _walletText.text = _wallet.ToString();
    }

    private void UseItem(ItemData item)
    {
        switch (item.ApplyEffect())
        {
            case ItemData.ActionEffect.Recovery:
                _playerController.UpdateStatus(1, 0, 0);
                break;
            case ItemData.ActionEffect.PowerUp:
                _playerController.UpdateStatus(0, 1, 0);
                break;
            case ItemData.ActionEffect.SpeedUp:
                _playerController.UpdateStatus(0, 0, 1);
                break;
        }

        _enemySpawner._count++;
    }

    public void OnStatusText()
    {
        _hpText.text = _playerController._status.Health.ToString();
        _atkText.text = _playerController._status.Attack.ToString();
        _spdText.text = _playerController._status.Speed.ToString();
    }

    public void AddWallet(int value)
    {
        _wallet += value;
        _walletText.text = _wallet.ToString();
    }

    public void OnStopTime(bool check)
    {
        switch (check)
        {
            case true:
                Time.timeScale = 0;
                break;
            case false:
                Time.timeScale = 1;
                break;
        }
    }
}