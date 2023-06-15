using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ArrowTargetEnemy : MonoBehaviour
{
    [SerializeField] private Transform _targetObject;    // 追従する3Dオブジェクト
    [SerializeField] private RectTransform _canvasRect;  // キャンバスのRectTransform

    private Image _arrowImage;          // 矢印のImageコンポーネント
    private Camera _mainCamera;         // メインカメラ

    private void Start()
    {
        _arrowImage = GetComponent<Image>();
        _mainCamera = Camera.main;      // メインカメラを取得
    }

    private void LateUpdate()
    {
        if (_targetObject == null || !_targetObject.gameObject.activeSelf || _mainCamera == null || _canvasRect == null)
        {
            return; // ターゲットオブジェクトが非アクティブまたは他の要件が満たされない場合、処理をスキップ
        }

        // 3Dオブジェクトがカメラ内に存在するかどうかを判定
        Vector3 screenPoint = _mainCamera.WorldToViewportPoint(_targetObject.position);
        bool isTargetVisible = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        if (isTargetVisible)
        {
            _arrowImage.enabled = false;   // カメラ内に表示されている場合は非表示にする
        }
        else
        {
            _arrowImage.enabled = true;    // カメラ外に表示されている場合は表示する

            // 3Dオブジェクトの方向を矢印に反映する
            Vector3 targetDirection = _targetObject.position - _mainCamera.transform.position;
            float angle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg;
            _arrowImage.rectTransform.rotation = Quaternion.Euler(0f, 0f, -angle);

            // 3Dオブジェクトのワールド座標をキャンバス内のスクリーン座標に変換
            Vector2 screenPosition = RectTransformUtility.WorldToScreenPoint(_mainCamera, _targetObject.position);
            Vector2 localPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, screenPosition, null, out localPosition);

            // キャンバス内に矢印を移動させる
            Vector2 clampedPosition = ClampToCanvas(localPosition);
            _arrowImage.rectTransform.localPosition = clampedPosition;
        }
    }

    // 位置をキャンバス内に制限する
    private Vector2 ClampToCanvas(Vector2 position)
    {
        Vector2 clampedPosition = position;

        // 矢印のサイズを考慮して、キャンバス内に制限する
        float arrowWidth = _arrowImage.rectTransform.rect.width;
        float arrowHeight = _arrowImage.rectTransform.rect.height;
        float canvasWidth = _canvasRect.rect.width;
        float canvasHeight = _canvasRect.rect.height;

        float minX = -canvasWidth / 2f + arrowWidth / 2f;
        float maxX = canvasWidth / 2f - arrowWidth / 2f;
        float minY = -canvasHeight / 2f + arrowHeight / 2f;
        float maxY = canvasHeight / 2f - arrowHeight / 2f;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);

        return clampedPosition;
    }
}
