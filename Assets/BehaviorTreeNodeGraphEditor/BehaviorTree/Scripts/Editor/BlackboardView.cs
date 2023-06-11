using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace BehaviorTreeNodeGraphEditor
{
    /// <summary>
    /// 行動ツリーエディタのブラックボードビューを表すクラス
    /// </summary>
    public class BlackboardView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<BlackboardView, VisualElement.UxmlTraits> { }

        public BlackboardView()
        {

        }

        /// <summary>
        /// シリアライズされた行動ツリーにブラックボードビューをバインド
        /// </summary>
        /// <param name="serializer"></param>
        internal void Bind(SerializedBehaviorTree serializer)
        {
            Clear();

            var blackboardProperty = serializer.Blackboard;

            blackboardProperty.isExpanded = true;

            // プロパティフィールド
            PropertyField field = new PropertyField();
            field.BindProperty(blackboardProperty);
            Add(field);
        }
    }
}