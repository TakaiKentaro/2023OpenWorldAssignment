using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

namespace BehaviorTreeNodeGraphEditor
{
    /// <summary>
    /// インスペクタービューのクラス
    /// </summary>
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits>
        {
        }

        public InspectorView()
        {
        }

        /// <summary>
        /// 選択されたノードの情報を更新
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="nodeView"></param>
        internal void UpdateSelection(SerializedBehaviorTree serializer, NodeView nodeView)
        {
            Clear();

            if (nodeView == null)
            {
                return;
            }

            var nodeProperty = serializer.FindNode(serializer.Nodes, nodeView.node);
            if (nodeProperty == null)
            {
                return;
            }

            // プロパティを自動的に展開
            nodeProperty.isExpanded = true;

            // プロパティフィールド
            PropertyField field = new PropertyField();
            field.label = nodeProperty.managedReferenceValue.GetType().ToString();
            field.BindProperty(nodeProperty);
            Add(field);
        }
    }
}