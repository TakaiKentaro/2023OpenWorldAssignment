using UnityEngine.UIElements;

namespace BehaviorTreeNodeGraphEditor
{
    /// <summary>
    /// 2つのパネルで構成される分割ビューを表すクラス
    /// </summary>
    public class SplitView : TwoPaneSplitView
    {
        /// <summary>
        /// UXMLファクトリクラス
        /// </summary>
        public new class UxmlFactory : UxmlFactory<SplitView, TwoPaneSplitView.UxmlTraits>
        {
        }
    }
}