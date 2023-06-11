using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

namespace BehaviorTreeNodeGraphEditor
{
    /// <summary>
    /// エディターのユーティリティクラス
    /// </summary>
    public static class EditorUtility
    {
        /// <summary>
        /// 新しい行動ツリーを作成
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static BehaviorTree CreateNewTree(string assetName, string folder)
        {
            string path = System.IO.Path.Join(folder, $"{assetName}.asset");
            if (System.IO.File.Exists(path))
            {
                Debug.LogError($"Failed to create behavior tree asset: Path already exists: {assetName}");
                return null;
            }

            BehaviorTree tree = ScriptableObject.CreateInstance<BehaviorTree>();
            tree.name = assetName;
            AssetDatabase.CreateAsset(tree, path);
            AssetDatabase.SaveAssets();
            EditorGUIUtility.PingObject(tree);
            return tree;
        }

        /// <summary>
        /// 指定された型のアセットを読み込み
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> LoadAssets<T>() where T : UnityEngine.Object
        {
            string[] assetIds = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
            List<T> assets = new List<T>();
            foreach (var assetId in assetIds)
            {
                string path = AssetDatabase.GUIDToAssetPath(assetId);
                T asset = AssetDatabase.LoadAssetAtPath<T>(path);
                assets.Add(asset);
            }

            return assets;
        }

        /// <summary>
        /// 指定された型のアセットのパスを取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<string> GetAssetPaths<T>() where T : UnityEngine.Object
        {
            string[] assetIds = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
            List<string> paths = new List<string>();
            foreach (var assetId in assetIds)
            {
                string path = AssetDatabase.GUIDToAssetPath(assetId);
                paths.Add(path);
            }

            return paths;
        }
    }
}