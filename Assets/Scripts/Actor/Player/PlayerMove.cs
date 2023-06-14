using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _turnSpeed = 0;
    [SerializeField] private float _isGroundedLength = 0;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        if (!IsGrounded())
        {
            return;
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = Vector3.forward * v + Vector3.right * h;

        if (dir == Vector3.zero)
        {
            _rb.velocity = new Vector3(0f, _rb.velocity.y, 0f);
        }
        else
        {
            dir.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(dir);
            this.transform.rotation =
                Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * _turnSpeed);

            Vector3 velo = dir.normalized * _moveSpeed;
            velo.y = _rb.velocity.y;
            _rb.velocity = velo;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        Vector3 start = this.transform.position + col.center;
        Vector3 end = start + Vector3.down * _isGroundedLength;
        Debug.DrawLine(start, end);
        bool isGrounded = Physics.Linecast(start, end);
        return isGrounded;
    }
}