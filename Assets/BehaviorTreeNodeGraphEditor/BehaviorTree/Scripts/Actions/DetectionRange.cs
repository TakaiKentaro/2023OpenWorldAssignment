using UnityEngine;
using BehaviorTreeNodeGraphEditor;

/// <summary>
/// 特定のオブジェクトが一定の範囲内に入ったら検知するクラス
/// </summary>
[System.Serializable]
public class DetectionRange : ActionNode
{
    public GameObject targetObject;
    public float detectionRange = 5f;

    private Transform _transform;

    protected override void OnStart()
    {
        _transform = context.transform;
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        // ターゲットオブジェクトが存在しない場合は失敗
        if (targetObject == null)
        {
            return State.Failure;
        }

        // ターゲットオブジェクトとの距離を計算
        float distance = Vector3.Distance(_transform.position, targetObject.transform.position);

        // 検知範囲内に入ったら成功
        if (distance <= detectionRange)
        {
            return State.Success;
        }

        return State.Running;
    }
}