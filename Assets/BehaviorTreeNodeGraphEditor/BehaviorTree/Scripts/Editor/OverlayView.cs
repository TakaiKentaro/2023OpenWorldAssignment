using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace BehaviorTreeNodeGraphEditor
{
    /// <summary>
    /// オーバーレイビューを表すクラス
    /// </summary>
    public class OverlayView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<OverlayView, UxmlTraits>
        {
        }

        public System.Action<BehaviorTree> OnTreeSelected;

        Button _openButton;
        Button _createButton;
        DropdownField _assetSelector;
        TextField _treeNameField;
        TextField _locationPathField;

        /// <summary>
        /// ビューを表示
        /// </summary>
        public void Show()
        {
            // UIBuilderで非表示にされている場合は表示
            style.visibility = Visibility.Visible;

            // フィールドを構成
            _openButton = this.Q<Button>("OpenButton");
            _assetSelector = this.Q<DropdownField>();
            _createButton = this.Q<Button>("CreateButton");
            _treeNameField = this.Q<TextField>("TreeName");
            _locationPathField = this.Q<TextField>("LocationPath");

            // アセット選択のドロップダウンメニューを構成
            var behaviorTrees = EditorUtility.GetAssetPaths<BehaviorTree>();
            _assetSelector.choices = new List<string>();
            behaviorTrees.ForEach(treePath => { _assetSelector.choices.Add(ToMenuFormat(treePath)); });

            // アセットを開くボタンを構成
            _openButton.clicked -= OnOpenAsset;
            _openButton.clicked += OnOpenAsset;

            // アセットを作成するボタンを構成
            _createButton.clicked -= OnCreateAsset;
            _createButton.clicked += OnCreateAsset;
        }

        /// <summary>
        /// ビューを非表示
        /// </summary>
        public void Hide()
        {
            style.visibility = Visibility.Hidden;
        }

        /// <summary>
        /// メニューフォーマットに変換
        /// </summary>
        public string ToMenuFormat(string one)
        {
            // スラッシュを使用してサブメニューを作成
            return one.Replace("/", "|");
        }

        /// <summary>
        /// アセットフォーマットに変換
        /// </summary>
        public string ToAssetFormat(string one)
        {
            // スラッシュを使用してサブメニューを作成する
            return one.Replace("|", "/");
        }

        private void OnOpenAsset()
        {
            string path = ToAssetFormat(_assetSelector.text);
            BehaviorTree tree = AssetDatabase.LoadAssetAtPath<BehaviorTree>(path);
            if (tree)
            {
                TreeSelected(tree);
                style.visibility = Visibility.Hidden;
            }
        }

        private void OnCreateAsset()
        {
            BehaviorTree tree = EditorUtility.CreateNewTree(_treeNameField.text, _locationPathField.text);
            if (tree)
            {
                TreeSelected(tree);
                style.visibility = Visibility.Hidden;
            }
        }

        private void TreeSelected(BehaviorTree tree)
        {
            OnTreeSelected.Invoke(tree);
        }
    }
}