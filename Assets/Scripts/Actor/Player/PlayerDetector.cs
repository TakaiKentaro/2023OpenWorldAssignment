using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private float _detectionRadius = 5f;
    [SerializeField] private LayerMask _enemyLayer;

    private bool _isDetected;
    private bool _wasDetected; // 前回の検知状態を保存する変数

    private void Update()
    {
        DetectEnemy();
    }

    private void DetectEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _detectionRadius, _enemyLayer);
        bool detected = colliders.Length > 0;

        if (detected && !_wasDetected)
        {
            _isDetected = true;
        }
        else if (!detected && _wasDetected)
        {
            _isDetected = false;
        }

        _wasDetected = detected; // 前回の検知状態を更新
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }

    public bool IsDetectEnemy()
    {
        return _isDetected;
    }
}