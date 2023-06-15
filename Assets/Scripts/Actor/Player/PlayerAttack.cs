using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerDetector))]
public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private WeaponAttack _weapon;
    private PlayerDetector _playerDetector;

    private void Start()
    {
        _playerDetector = GetComponent<PlayerDetector>();
    }

    private void Update()
    {
        
        if (!_playerDetector.IsDetectEnemy())
        {
            return;
        }
        _weapon.OnAttack(transform);
    }
}