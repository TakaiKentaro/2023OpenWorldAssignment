using UnityEngine;
using BehaviorTreeNodeGraphEditor;

[System.Serializable]
public class Breakpoint : ActionNode
{
    /// <summary>
    /// ノードの開始時に呼ばれるメソッド
    /// </summary>
    protected override void OnStart()
    {
        Debug.Log("Triggering Breakpoint");
        Debug.Break();
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
        return State.Success;
    }
}