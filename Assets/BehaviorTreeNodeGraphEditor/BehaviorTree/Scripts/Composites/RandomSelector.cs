using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace BehaviorTreeNodeGraphEditor
{
    /// <summary>
    /// ランダムに子ノードを選択するセレクターノードを表すクラス
    /// </summary>
    [System.Serializable]
    public class RandomSelector : CompositeNode
    {
        private int current;

        protected override void OnStart()
        {
            current = Random.Range(0, children.Count);
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            var child = children[current];
            return child.Update();
        }
    }
}