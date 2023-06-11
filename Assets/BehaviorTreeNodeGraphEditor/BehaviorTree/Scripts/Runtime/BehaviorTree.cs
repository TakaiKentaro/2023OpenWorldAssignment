using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BehaviorTreeNodeGraphEditor
{
    /// <summary>
    /// 行動ツリーを表すスクリプタブルオブジェクト
    /// </summary>
    [CreateAssetMenu()]
    public class BehaviorTree : ScriptableObject
    {
        [SerializeReference] public RootNode rootNode;

        [SerializeReference] public List<Node> nodes = new List<Node>();

        public Node.State treeState = Node.State.Running;

        public Blackboard blackboard = new Blackboard();

        #region EditorProperties

        //エディタ上のビューの位置を表すベクトル
        public Vector3 viewPosition = new Vector3(600, 300);

        //エディタ上のビューのスケールを表すベクトル
        public Vector3 viewScale = Vector3.one;

        #endregion

        public BehaviorTree()
        {
            rootNode = new RootNode();
            nodes.Add(rootNode);
        }

        /// <summary>
        /// 行動ツリーの更新
        /// </summary>
        /// <returns>行動ツリーの状態</returns>
        public Node.State Update()
        {
            if (rootNode.state == Node.State.Running)
            {
                treeState = rootNode.Update();
            }

            return treeState;
        }

        /// <summary>
        /// 指定された親ノードの子ノードリストを取得
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static List<Node> GetChildren(Node parent)
        {
            List<Node> children = new List<Node>();

            if (parent is DecoratorNode decorator && decorator.child != null)
            {
                children.Add(decorator.child);
            }

            if (parent is RootNode rootNode && rootNode.child != null)
            {
                children.Add(rootNode.child);
            }

            if (parent is CompositeNode composite)
            {
                return composite.children;
            }

            return children;
        }

        /// <summary>
        /// 行動ツリーをトラバースし、指定されたアクションを実行
        /// </summary>
        /// <param name="node"></param>
        /// <param name="visiter"></param>
        public static void Traverse(Node node, System.Action<Node> visiter)
        {
            if (node != null)
            {
                visiter.Invoke(node);
                var children = GetChildren(node);
                children.ForEach((n) => Traverse(n, visiter));
            }
        }

        /// <summary>
        /// 行動ツリーのクローンを作成
        /// </summary>
        /// <returns></returns>
        public BehaviorTree Clone()
        {
            BehaviorTree tree = Instantiate(this);
            return tree;
        }

        /// <summary>
        /// コンテキストと行動ツリーをバインド
        /// </summary>
        /// <param name="context"></param>
        public void Bind(Context context)
        {
            Traverse(rootNode, node =>
            {
                node.context = context;
                node.blackboard = blackboard;
            });
        }
    }
}