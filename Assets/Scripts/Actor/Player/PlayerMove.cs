using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _isGroundedLength = 0;
    [SerializeField] private GameObject _particlePrefab; // パーティクルのプレハブ

    private Rigidbody _rb;
    private bool _isMove;
    private Vector3 _targetPosition;
    private GameObject _currentParticleEffect; // 現在のパーティクルエフェクト

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_isMove)
        {
            MoveToTarget();
        }
        else
        {
            Move();
        }

        Jump();

        if (Input.GetMouseButtonDown(0))
        {
            SetTargetPosition();
        }
    }

    private void Move()
    {
        if (!IsGrounded())
        {
            return;
        }

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        _rb.velocity = input.magnitude > 0 ? new Vector3(0f, _rb.velocity.y, 0f) : _rb.velocity;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        Vector3 start = transform.position + col.center;
        Vector3 end = start + Vector3.down * _isGroundedLength;
        Debug.DrawLine(start, end);
        return Physics.Linecast(start, end);
    }

    private void SetTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            _targetPosition = hit.point;
            _isMove = true;

            // 目的地に新しいパーティクルを生成する
            if (_currentParticleEffect != null)
            {
                Destroy(_currentParticleEffect); // 既存のパーティクルを破棄する
            }

            // 敵のタグが付いたオブジェクトかどうかを判定してパーティクルの色を設定する
            if (hit.collider.CompareTag("Enemy"))
            {
                _currentParticleEffect = Instantiate(_particlePrefab, hit.collider.transform.position + Vector3.up * 2f, Quaternion.identity, hit.collider.transform);
                var particleRenderer = _currentParticleEffect.GetComponent<ParticleSystemRenderer>();
                particleRenderer.material.color = Color.red; // 赤色に設定する
            }
            else
            {
                _currentParticleEffect = Instantiate(_particlePrefab, _targetPosition, Quaternion.identity);
                var particleRenderer = _currentParticleEffect.GetComponent<ParticleSystemRenderer>();
                particleRenderer.material.color = Color.yellow; // 青色に設定する
            }
        }
    }

    private void MoveToTarget()
    {
        Vector3 dir = _targetPosition - transform.position;
        dir.y = 0;

        if (dir.magnitude <= 0.1f)
        {
            _isMove = false;
            if (_currentParticleEffect != null)
            {
                Destroy(_currentParticleEffect); // 目的地に到着したらパーティクルを破棄する
            }
            return;
        }

        transform.rotation = Quaternion.LookRotation(dir);

        Vector3 velocity = dir.normalized * _moveSpeed;
        velocity.y = _rb.velocity.y;
        _rb.velocity = velocity;

        if (Vector3.Distance(transform.position, _targetPosition) <= 0.1f)
        {
            _isMove = false;
            transform.position = _targetPosition;
            if (_currentParticleEffect != null)
            {
                Destroy(_currentParticleEffect); // 目的地に到着したらパーティクルを破棄する
            }
        }
    }
}
