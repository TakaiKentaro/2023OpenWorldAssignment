using UnityEngine;

namespace BehaviorTreeNodeGraphEditor
{
    /// <summary>
    /// アクションノードの中で指定された時間待機するウェイトクラス
    /// </summary>
    [System.Serializable]
    public class Wait : ActionNode
    {
        /// <summary>
        /// 待機する時間（秒）
        /// </summary>
        public float duration = 1;

        private float startTime;

        /// <summary>
        /// ノードが開始された時に呼び出す
        /// </summary>
        protected override void OnStart()
        {
            startTime = Time.time;
        }

        /// <summary>
        /// ノードが停止した時に呼び出す
        /// </summary>
        protected override void OnStop()
        {
        }

        /// <summary>
        /// ノードの状態を更新
        /// </summary>
        /// <returns></returns>
        protected override State OnUpdate()
        {
            float elapsedTime = Time.time - startTime;
            if (elapsedTime > duration)
            {
                return State.Success;
            }

            return State.Running;
        }
    }
}