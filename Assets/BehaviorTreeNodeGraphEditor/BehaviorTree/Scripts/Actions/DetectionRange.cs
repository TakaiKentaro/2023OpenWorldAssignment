using UnityEngine;
using BehaviorTreeNodeGraphEditor;
using UnityEngine.Serialization;

/// <summary>
/// 範囲内に入った時にPlayerControllerの位置をblackboard.moveToPositionに渡すノード
/// </summary>
[System.Serializable]
public class DetectionRange : ActionNode
{
    public float _distance = 5f;
    public PlayerController _playerController;
    /// <summary>
    /// ノードの開始時に呼ばれるメソッド
    /// </summary>
    protected override void OnStart()
    {
        _playerController = context.playerController;
    }

    /// <summary>
    /// ノードの停止時に呼ばれるメソッド
    /// </summary>
    protected override void OnStop()
    {
    }

    /// <summary>
    /// ノードの更新時に呼ばれるメソッド
    /// </summary>
    /// <returns></returns>
    protected override State OnUpdate()
    {
        if (_playerController == null)
        {
            return State.Failure;
        }
        
        float playerDis = Vector3.Distance(context.transform.position, _playerController.transform.position);

        if (playerDis <= _distance)
        {
            blackboard.moveToPosition = _playerController.transform.position;
            return State.Success;
        }
        
        return State.Running;
    }
}