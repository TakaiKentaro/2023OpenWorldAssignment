using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Callbacks;

namespace BehaviorTreeNodeGraphEditor
{
    public class BehaviorTreeEditorWindow : EditorWindow
    {
        public class Test : UnityEditor.AssetModificationProcessor
        {
            static AssetDeleteResult OnWillDeleteAsset(string path, RemoveAssetOptions opt)
            {
                BehaviorTreeEditorWindow wnd = GetWindow<BehaviorTreeEditorWindow>();
                wnd.ClearIfSelected(path);
                return AssetDeleteResult.DidNotDelete;
            }
        }

        SerializedBehaviorTree serializer;
        BehaviorTreeSettings settings;

        BehaviorTreeView treeView;
        InspectorView inspectorView;
        BlackboardView blackboardView;
        OverlayView overlayView;
        ToolbarMenu toolbarMenu;
        Label titleLabel;

        [MenuItem("BehaviorTreeNodeGraphEditor/BehaviorTreeEditor ...")]
        public static void OpenWindow()
        {
            BehaviorTreeEditorWindow wnd = GetWindow<BehaviorTreeEditorWindow>();
            wnd.titleContent = new GUIContent("BehaviorTreeEditor");
            wnd.minSize = new Vector2(800, 600);
        }

        public static void OpenWindow(BehaviorTree tree)
        {
            BehaviorTreeEditorWindow wnd = GetWindow<BehaviorTreeEditorWindow>();
            wnd.titleContent = new GUIContent("BehaviorTreeEditor");
            wnd.minSize = new Vector2(800, 600);
            wnd.SelectTree(tree);
        }

        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceId, int line)
        {
            if (Selection.activeObject is BehaviorTree)
            {
                OpenWindow(Selection.activeObject as BehaviorTree);
                return true;
            }

            return false;
        }

        public void CreateGUI()
        {
            settings = BehaviorTreeSettings.GetOrCreateSettings();

            VisualElement root = rootVisualElement;

            var visualTree = settings.behaviourTreeXml;
            visualTree.CloneTree(root);

            var styleSheet = settings.behaviourTreeStyle;
            root.styleSheets.Add(styleSheet);

            treeView = root.Q<BehaviorTreeView>();
            inspectorView = root.Q<InspectorView>();
            blackboardView = root.Q<BlackboardView>();
            toolbarMenu = root.Q<ToolbarMenu>();
            overlayView = root.Q<OverlayView>("OverlayView");
            titleLabel = root.Q<Label>("TitleLabel");

            toolbarMenu.RegisterCallback<MouseEnterEvent>((evt) =>
            {
                toolbarMenu.menu.MenuItems().Clear();
                var behaviorTrees = EditorUtility.GetAssetPaths<BehaviorTree>();
                behaviorTrees.ForEach(path =>
                {
                    var fileName = System.IO.Path.GetFileName(path);
                    toolbarMenu.menu.AppendAction($"{fileName}", (a) =>
                    {
                        var tree = AssetDatabase.LoadAssetAtPath<BehaviorTree>(path);
                        SelectTree(tree);
                    });
                });
                toolbarMenu.menu.AppendSeparator();
                toolbarMenu.menu.AppendAction("New Tree...", (a) => OnToolbarNewAsset());
            });

            treeView.OnNodeSelected = OnNodeSelectionChanged;
            overlayView.OnTreeSelected += SelectTree;
            Undo.undoRedoPerformed += OnUndoRedo;

            if (serializer == null)
            {
                overlayView.Show();
            }
            else
            {
                SelectTree(serializer.tree);
            }
        }

        void OnUndoRedo()
        {
            if (serializer != null)
            {
                treeView.PopulateView(serializer);
            }
        }

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange obj)
        {
            switch (obj)
            {
                case PlayModeStateChange.EnteredEditMode:
                    EditorApplication.delayCall += OnSelectionChange;
                    break;
                case PlayModeStateChange.ExitingEditMode:
                    break;
                case PlayModeStateChange.EnteredPlayMode:
                    EditorApplication.delayCall += OnSelectionChange;
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    break;
            }
        }

        private void OnSelectionChange()
        {
            if (Selection.activeGameObject)
            {
                BehaviorTreeRunner runner = Selection.activeGameObject.GetComponent<BehaviorTreeRunner>();
                if (runner)
                {
                    SelectTree(runner._behaviorTree);
                }
            }
        }

        void SelectTree(BehaviorTree newTree)
        {
            if (!newTree)
            {
                ClearSelection();
                return;
            }

            serializer = new SerializedBehaviorTree(newTree);

            if (titleLabel != null)
            {
                string path = AssetDatabase.GetAssetPath(serializer.tree);
                if (path == "")
                {
                    path = serializer.tree.name;
                }

                titleLabel.text = $"TreeView ({path})";
            }

            overlayView.Hide();
            treeView.PopulateView(serializer);
            blackboardView.Bind(serializer);
        }

        void ClearSelection()
        {
            serializer = null;
            overlayView.Show();
            treeView.ClearView();
        }

        void ClearIfSelected(string path)
        {
            if (AssetDatabase.GetAssetPath(serializer.tree) == path)
            {
                EditorApplication.delayCall += () => { SelectTree(null); };
            }
        }

        void OnNodeSelectionChanged(NodeView node)
        {
            inspectorView.UpdateSelection(serializer, node);
        }

        private void OnInspectorUpdate()
        {
            treeView?.UpdateNodeStates();
        }

        void OnToolbarNewAsset()
        {
            BehaviorTree tree = EditorUtility.CreateNewTree("New Behavior Tree", "Assets/");
            if (tree)
            {
                SelectTree(tree);
            }
        }
    }
}