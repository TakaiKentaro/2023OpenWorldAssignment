using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform _transform;
    private Vector3 _movement;

    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        // 移動ベクトルの初期化
        _movement = Vector3.zero;

        // 入力の取得
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 入力に基づく移動ベクトルの設定
        _movement.x = horizontalInput;
        _movement.z = verticalInput;
        _movement.Normalize();

        // 移動の適用
        Move(_movement);

        // 方向転換
        if (_movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_movement);
            _transform.rotation = Quaternion.Lerp(_transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void Move(Vector3 direction)
    {
        _transform.position += direction * moveSpeed * Time.deltaTime;
    }
}