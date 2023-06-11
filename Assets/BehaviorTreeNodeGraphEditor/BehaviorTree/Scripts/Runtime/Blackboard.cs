using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreeNodeGraphEditor
{
    /// <summary>
    /// すべてのノード間で共有されるブラックボードのコンテナ
    /// 複数のノードが読み書きアクセスするための一時データを格納するために使用
    /// 特定のユースケースに合わせて他のプロパティをここに追加
    /// </summary>
    [System.Serializable]
    public class Blackboard
    {
        //移動先の位置を表すベクトル
        public Vector3 moveToPosition;
    }
}