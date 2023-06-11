using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace BehaviorTreeNodeGraphEditor
{
    /// <summary>
    /// 並列処理を行うコンポジットノードを表すクラス
    /// </summary>
    [System.Serializable]
    public class Parallel : CompositeNode
    {
        private List<State> childrenLeftToExecute = new List<State>();

        protected override void OnStart()
        {
            childrenLeftToExecute.Clear();
            children.ForEach(child => { childrenLeftToExecute.Add(State.Running); });
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            bool stillRunning = false;
            for (int i = 0; i < childrenLeftToExecute.Count; ++i)
            {
                if (childrenLeftToExecute[i] == State.Running)
                {
                    var status = children[i].Update();
                    if (status == State.Failure)
                    {
                        AbortRunningChildren();
                        return State.Failure;
                    }

                    if (status == State.Running)
                    {
                        stillRunning = true;
                    }

                    childrenLeftToExecute[i] = status;
                }
            }

            return stillRunning ? State.Running : State.Success;
        }

        private void AbortRunningChildren()
        {
            for (int i = 0; i < childrenLeftToExecute.Count; ++i)
            {
                if (childrenLeftToExecute[i] == State.Running)
                {
                    children[i].Abort();
                }
            }
        }
    }
}