using UnityEngine;
using BehaviorTreeNodeGraphEditor;

/// <summary>
/// アクションノードの中でランダムな位置を設定するランダムポジションクラス
/// </summary>
[System.Serializable]
public class RandomPosition : ActionNode
{
    /// <summary>
    /// ランダムな位置の最小値
    /// </summary>
    public Vector2 min = Vector2.one * -10;

    /// <summary>
    /// ランダムな位置の最大値
    /// </summary>
    public Vector2 max = Vector2.one * 10;

    /// <summary>
    /// ノードが開始された時に呼び出す
    /// </summary>
    protected override void OnStart()
    {
    }

    /// <summary>
    /// ノードが停止した時に呼び出す
    /// </summary>
    protected override void OnStop()
    {
    }

    /// <summary>
    /// ノードの状態を更新
    /// </summary>
    /// <returns></returns>
    protected override State OnUpdate()
    {
        Vector3 position = new Vector3();
        position.x = Random.Range(min.x, max.x);
        position.y = Random.Range(min.y, max.y);
        blackboard.moveToPosition = position;
        return State.Success;
    }
}