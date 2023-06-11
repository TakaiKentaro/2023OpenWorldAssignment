using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreeNodeGraphEditor
{
    /// <summary>
    /// 子ノードを繰り返し実行するデコレーターノードを表すクラス
    /// </summary>
    [System.Serializable]
    public class Repeat : DecoratorNode
    {
        // 成功時に子ノードを再起動するかどうかを示すフラグです。
        public bool restartOnSuccess = true;

        //失敗時に子ノードを再起動するかどうかを示すフラグ
        public bool restartOnFailure = false;

        protected override void OnStart()
        {
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            switch (child.Update())
            {
                case State.Running:
                    break;
                case State.Failure:
                    if (restartOnFailure)
                    {
                        return State.Running;
                    }
                    else
                    {
                        return State.Failure;
                    }
                case State.Success:
                    if (restartOnSuccess)
                    {
                        return State.Running;
                    }
                    else
                    {
                        return State.Success;
                    }
            }

            return State.Running;
        }
    }
}