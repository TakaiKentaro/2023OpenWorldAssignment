using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace BehaviorTreeNodeGraphEditor
{
    /// <summary>
    /// ダブルクリックによる選択を処理するマウス操作クラス
    /// </summary>
    public class DoubleClickSelection : MouseManipulator
    {
        double time;
        double doubleClickDuration = 0.3;

        public DoubleClickSelection()
        {
            time = EditorApplication.timeSinceStartup;
        }

        protected override void RegisterCallbacksOnTarget()
        {
            target.RegisterCallback<MouseDownEvent>(OnMouseDown);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            target.UnregisterCallback<MouseDownEvent>(OnMouseDown);
        }

        /// <summary>
        /// マウスのダウンイベント時に呼び出され、ダブルクリックの検出と子要素の選択を行う
        /// </summary>
        /// <param name="evt"></param>
        private void OnMouseDown(MouseDownEvent evt)
        {
            var graphView = target as BehaviorTreeView;
            if (graphView == null)
                return;

            double duration = EditorApplication.timeSinceStartup - time;
            if (duration < doubleClickDuration)
            {
                SelectChildren(evt);
            }

            time = EditorApplication.timeSinceStartup;
        }

        /// <summary>
        ///  ダブルクリックされた要素の子要素を選択
        /// </summary>
        /// <param name="evt"></param>
        void SelectChildren(MouseDownEvent evt)
        {
            var graphView = target as BehaviorTreeView;
            if (graphView == null)
                return;

            if (!CanStopManipulation(evt))
                return;

            NodeView clickedElement = evt.target as NodeView;
            if (clickedElement == null)
            {
                var ve = evt.target as VisualElement;
                clickedElement = ve.GetFirstAncestorOfType<NodeView>();
                if (clickedElement == null)
                    return;
            }

            // ルート要素も移動可能にするため、子要素を選択に追加
            BehaviorTree.Traverse(clickedElement.node, node =>
            {
                var view = graphView.FindNodeView(node);
                graphView.AddToSelection(view);
            });
        }
    }
}