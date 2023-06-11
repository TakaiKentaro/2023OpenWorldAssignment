using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreeNodeGraphEditor
{
    /// <summary>
    /// 子ノードを持つコンポジットノードの抽象クラス
    /// </summary>
    [System.Serializable]
    public abstract class CompositeNode : Node
    {
        // 子ノードのリスト
        [HideInInspector] [SerializeReference] public List<Node> children = new List<Node>();
    }
}