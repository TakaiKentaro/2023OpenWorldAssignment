using UnityEngine;

namespace BehaviorTreeNodeGraphEditor
{
    /// <summary>
    /// アクションノードの中で一定の確率で失敗するランダムな失敗クラス
    /// </summary>
    [System.Serializable]
    public class RandomFailure : ActionNode
    {
        /// <summary>
        /// 失敗する確率（0から1の範囲内）
        /// </summary>
        [Range(0, 1)] public float chanceOfFailure = 0.5f;

        /// <summary>
        /// ノードが開始された時に呼び出す
        /// </summary>
        protected override void OnStart()
        {
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
            float randomValue = Random.value;
            if (randomValue > chanceOfFailure)
            {
                return State.Failure;
            }

            return State.Success;
        }
    }
}