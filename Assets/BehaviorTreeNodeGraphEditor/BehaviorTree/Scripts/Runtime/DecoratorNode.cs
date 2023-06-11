using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreeNodeGraphEditor
{
    /// <summary>
    /// デコレーターノードの基底クラス
    /// </summary>
    public abstract class DecoratorNode : Node
    {
        [SerializeReference] [HideInInspector] public Node child;
    }
}