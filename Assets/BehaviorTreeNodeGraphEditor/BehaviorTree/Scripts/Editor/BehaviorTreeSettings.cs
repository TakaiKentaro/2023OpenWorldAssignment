using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

// 設定アセットの新しいタイプを作成します。
class BehaviorTreeSettings : ScriptableObject
{
    public VisualTreeAsset behaviourTreeXml;
    public StyleSheet behaviourTreeStyle;
    public VisualTreeAsset nodeXml;
    public TextAsset scriptTemplateActionNode;
    public TextAsset scriptTemplateCompositeNode;
    public TextAsset scriptTemplateDecoratorNode;
    public string newNodeBasePath = "Assets/";

    // 設定を探します。
    static BehaviorTreeSettings FindSettings()
    {
        var guids = AssetDatabase.FindAssets("t:BehaviorTreeSettings");
        if (guids.Length > 1)
        {
            Debug.LogWarning("複数の設定ファイルが見つかりました。最初のものを使用します。");
        }

        switch (guids.Length)
        {
            case 0:
                return null;
            default:
                var path = AssetDatabase.GUIDToAssetPath(guids[0]);
                return AssetDatabase.LoadAssetAtPath<BehaviorTreeSettings>(path);
        }
    }

    // 設定を取得または作成します。
    internal static BehaviorTreeSettings GetOrCreateSettings()
    {
        var settings = FindSettings();
        if (settings == null)
        {
            settings = ScriptableObject.CreateInstance<BehaviorTreeSettings>();
            AssetDatabase.CreateAsset(settings, "Assets");
            AssetDatabase.SaveAssets();
        }

        return settings;
    }

    // シリアライズされた設定を取得します。
    internal static SerializedObject GetSerializedSettings()
    {
        return new SerializedObject(GetOrCreateSettings());
    }
}

// 描画フレームワークのためにUIElementsを使用してSettingsProviderを登録します。
static class MyCustomSettingsUIElementsRegister
{
    [SettingsProvider]
    public static SettingsProvider CreateMyCustomSettingsProvider()
    {
        // 最初のパラメータはSettingsウィンドウ内のパスです。
        // 2番目のパラメータはこの設定のスコープです。プロジェクトスコープのSettingsウィンドウにのみ表示されます。
        var provider = new SettingsProvider("Project/MyCustomUIElementsSettings", SettingsScope.Project)
        {
            label = "BehaviorTree",
            // activateHandlerは、ユーザーが設定ウィンドウのアイテムをクリックしたときに呼び出されます。
            activateHandler = (searchContext, rootElement) =>
            {
                var settings = BehaviorTreeSettings.GetSerializedSettings();
                // rootElementはVisualElementです。子要素を追加すると、OnGUI関数は呼び出されません。
                // これは、SettingsProviderがUIElementsの描画フレームワークを使用するためです。
                var title = new Label()
                {
                    text = "Behavior Tree Settings"
                };
                title.AddToClassList("title");
                rootElement.Add(title);

                var properties = new VisualElement()
                {
                    style =
                    {
                        flexDirection = FlexDirection.Column
                    }
                };
                properties.AddToClassList("property-list");
                rootElement.Add(properties);

                properties.Add(new InspectorElement(settings));

                rootElement.Bind(settings);
            },
        };

        return provider;
    }
}