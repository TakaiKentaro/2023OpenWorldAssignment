using UnityEngine;
using BehaviorTreeNodeGraphEditor;

/// <summary>
/// このアクションノードは、エージェントをNavMesh上の指定された位置に移動
/// </summary>
[System.Serializable]
public class MoveToPosition : ActionNode
{
    public float speed = 5;
    public float stoppingDistance = 0.1f;
    public bool updateRotation = true;
    public float acceleration = 40.0f;
    public float tolerance = 1.0f;

    /// <summary>
    /// ノードの開始時に呼ばれるメソッド
    /// </summary>
    protected override void OnStart()
    {
        context.agent.stoppingDistance = stoppingDistance;
        context.agent.speed = speed;
        context.agent.destination = blackboard.moveToPosition;
        context.agent.updateRotation = updateRotation;
        context.agent.acceleration = acceleration;
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
        if (context.agent.pathPending)
        {
            return State.Running;
        }

        if (context.agent.remainingDistance < tolerance)
        {
            return State.Success;
        }

        if (context.agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathInvalid)
        {
            return State.Failure;
        }

        return State.Running;
    }
}