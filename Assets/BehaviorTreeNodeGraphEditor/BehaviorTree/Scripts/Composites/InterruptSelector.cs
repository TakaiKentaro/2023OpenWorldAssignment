using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreeNodeGraphEditor
{
    /// <summary>
    /// 割り込みを処理するセレクターノードを表すクラス
    /// </summary>
    [System.Serializable]
    public class InterruptSelector : Selector
    {
        protected override State OnUpdate()
        {
            int previousIndex = current;
            base.OnStart();
            var status = base.OnUpdate();
            if (previousIndex != current)
            {
                if (children[previousIndex].state == State.Running)
                {
                    children[previousIndex].Abort();
                }
            }

            return status;
        }
    }
}