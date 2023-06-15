using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 10f;

    private Vector3 _rotationAxis;  // 回転軸のワールド座標

    private void Start()
    {
        _rotationAxis = Vector3.up;  // 回転軸をワールド上方向に設定
    }

    public void OnAttack(Transform pivot)
    {
        transform.RotateAround(pivot.position, _rotationAxis, _rotationSpeed * Time.deltaTime);
    }
}