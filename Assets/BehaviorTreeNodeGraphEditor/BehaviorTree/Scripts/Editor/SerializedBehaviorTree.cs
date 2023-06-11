using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BehaviorTreeNodeGraphEditor
{
    /// <summary>
    /// シリアライズされたビヘイビアツリーを表すクラス
    /// </summary>
    public class SerializedBehaviorTree
    {
        readonly public SerializedObject serializedObject;
        readonly public BehaviorTree tree;

        // SerializedPropertyのプロパティ名
        const string sPropRootNode = "rootNode";
        const string sPropNodes = "nodes";
        const string sPropBlackboard = "blackboard";
        const string sPropGuid = "guid";
        const string sPropChild = "child";
        const string sPropChildren = "children";
        const string sPropPosition = "position";
        const string sViewTransformPosition = "viewPosition";
        const string sViewTransformScale = "viewScale";

        public SerializedProperty RootNode
        {
            get { return serializedObject.FindProperty(sPropRootNode); }
        }

        public SerializedProperty Nodes
        {
            get { return serializedObject.FindProperty(sPropNodes); }
        }

        public SerializedProperty Blackboard
        {
            get { return serializedObject.FindProperty(sPropBlackboard); }
        }

        public SerializedBehaviorTree(BehaviorTree tree)
        {
            serializedObject = new SerializedObject(tree);
            this.tree = tree;
        }

        /// <summary>
        /// 変更を保存
        /// </summary>
        public void Save()
        {
            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// 指定されたノードを見る
        /// </summary>
        public SerializedProperty FindNode(SerializedProperty array, Node node)
        {
            for (int i = 0; i < array.arraySize; ++i)
            {
                var current = array.GetArrayElementAtIndex(i);
                if (current.FindPropertyRelative(sPropGuid).stringValue == node.guid)
                {
                    return current;
                }
            }

            return null;
        }

        /// <summary>
        /// ビュートランスフォームを設定
        /// </summary>
        public void SetViewTransform(Vector3 position, Vector3 scale)
        {
            serializedObject.FindProperty(sViewTransformPosition).vector3Value = position;
            serializedObject.FindProperty(sViewTransformScale).vector3Value = scale;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }

        /// <summary>
        /// ノードの位置を設定
        /// </summary>
        public void SetNodePosition(Node node, Vector2 position)
        {
            var nodeProp = FindNode(Nodes, node);
            nodeProp.FindPropertyRelative(sPropPosition).vector2Value = position;
            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// ノードを削除
        /// </summary>
        public void DeleteNode(SerializedProperty array, Node node)
        {
            for (int i = 0; i < array.arraySize; ++i)
            {
                var current = array.GetArrayElementAtIndex(i);
                if (current.FindPropertyRelative(sPropGuid).stringValue == node.guid)
                {
                    array.DeleteArrayElementAtIndex(i);
                    return;
                }
            }
        }

        /// <summary>
        /// ノードのインスタンスを作成
        /// </summary>
        public Node CreateNodeInstance(System.Type type)
        {
            Node node = System.Activator.CreateInstance(type) as Node;
            node.guid = GUID.Generate().ToString();
            return node;
        }

        /// <summary>
        /// 配列に要素を追加
        /// </summary>
        SerializedProperty AppendArrayElement(SerializedProperty arrayProperty)
        {
            arrayProperty.InsertArrayElementAtIndex(arrayProperty.arraySize);
            return arrayProperty.GetArrayElementAtIndex(arrayProperty.arraySize - 1);
        }

        /// <summary>
        /// ノードを作成
        /// </summary>
        public Node CreateNode(System.Type type, Vector2 position)
        {
            Node node = CreateNodeInstance(type);
            node.position = position;

            SerializedProperty newNode = AppendArrayElement(Nodes);
            newNode.managedReferenceValue = node;

            serializedObject.ApplyModifiedProperties();

            return node;
        }

        /// <summary>
        /// ルートノードを設定
        /// </summary>
        public void SetRootNode(RootNode node)
        {
            RootNode.managedReferenceValue = node;
            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// ノードを削除
        /// </summary>
        public void DeleteNode(Node node)
        {
            SerializedProperty nodesProperty = Nodes;

            for (int i = 0; i < nodesProperty.arraySize; ++i)
            {
                var prop = nodesProperty.GetArrayElementAtIndex(i);
                var guid = prop.FindPropertyRelative(sPropGuid).stringValue;
                DeleteNode(Nodes, node);
                serializedObject.ApplyModifiedProperties();
            }
        }

        /// <summary>
        /// 子ノードを追加
        /// </summary>
        public void AddChild(Node parent, Node child)
        {
            var parentProperty = FindNode(Nodes, parent);

            var childProperty = parentProperty.FindPropertyRelative(sPropChild);
            if (childProperty != null)
            {
                childProperty.managedReferenceValue = child;
                serializedObject.ApplyModifiedProperties();
                return;
            }

            var childrenProperty = parentProperty.FindPropertyRelative(sPropChildren);
            if (childrenProperty != null)
            {
                SerializedProperty newChild = AppendArrayElement(childrenProperty);
                newChild.managedReferenceValue = child;
                serializedObject.ApplyModifiedProperties();
                return;
            }
        }

        /// <summary>
        /// 子ノードを削除
        /// </summary>
        public void RemoveChild(Node parent, Node child)
        {
            var parentProperty = FindNode(Nodes, parent);

            var childProperty = parentProperty.FindPropertyRelative(sPropChild);
            if (childProperty != null)
            {
                childProperty.managedReferenceValue = null;
                serializedObject.ApplyModifiedProperties();
                return;
            }

            var childrenProperty = parentProperty.FindPropertyRelative(sPropChildren);
            if (childrenProperty != null)
            {
                DeleteNode(childrenProperty, child);
                serializedObject.ApplyModifiedProperties();
                return;
            }
        }
    }
}