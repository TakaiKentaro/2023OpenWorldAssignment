using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreeNodeGraphEditor
{
    /// <summary>
    /// 実行時間の制限を持つデコレーターノードを表すクラス
    /// </summary>
    [System.Serializable]
    public class Timeout : DecoratorNode
    {
        public float duration = 1.0f;
        float startTime;

        protected override void OnStart()
        {
            startTime = Time.time;
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (Time.time - startTime > duration)
            {
                return State.Failure;
            }

            return child.Update();
        }
    }
}