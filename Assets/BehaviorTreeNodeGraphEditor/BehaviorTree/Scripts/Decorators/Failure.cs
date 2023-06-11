using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreeNodeGraphEditor
{
    /// <summary>
    /// 失敗時に子ノードを実行するデコレーターノードを表すクラス
    /// </summary>
    [System.Serializable]
    public class Failure : DecoratorNode
    {
        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            var state = child.Update();
            if (state == State.Success)
            {
                return State.Failure;
            }

            return state;
        }
    }
}