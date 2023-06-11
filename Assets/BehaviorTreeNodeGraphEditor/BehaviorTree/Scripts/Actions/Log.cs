using UnityEngine;

namespace BehaviorTreeNodeGraphEditor
{
    [System.Serializable]
    public class Log : ActionNode
    {
        public string message;

        /// <summary>
        /// ノードの開始時に呼ばれるメソッド
        /// </summary>
        protected override void OnStart()
        {
        }

        /// <summary>
        /// ノードの停止時に呼ばれるメソッド
        /// </summary>
        protected override void OnStop()
        {
        }

        /// <summary>
        /// ノードの更新時に呼ばれるメソッド
        /// </summary>
        /// <returns></returns>
        protected override State OnUpdate()
        {
            Debug.Log($"{message}");
            return State.Success;
        }
    }
}