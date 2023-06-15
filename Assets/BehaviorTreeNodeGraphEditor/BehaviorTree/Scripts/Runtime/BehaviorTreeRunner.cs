using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace BehaviorTreeNodeGraphEditor
{
    /// <summary>
    /// 行動ツリーを実行するためのランナークラス
    /// </summary>
    public class BehaviorTreeRunner : MonoBehaviour
    {
        [Tooltip("メインの行動ツリーアセット")] public BehaviorTree _behaviorTree;

        private PlayerController _playerController;
        // ゲームオブジェクトのサブシステムを保持するコンテキストオブジェクト
        Context _context;

        void Start()
        {
            
            _playerController = GameObject.FindObjectOfType<PlayerController>();
            _context = CreateBehaviourTreeContext();
            if (_playerController != null)
            {
                _context.playerController = _playerController;
            }
            _behaviorTree = _behaviorTree.Clone();
            _behaviorTree.Bind(_context);
        }

        void Update()
        {
            if (_behaviorTree)
            {
                _behaviorTree.Update();
            }
        }

        /// <summary>
        /// 行動ツリー用のコンテキストを作成
        /// </summary>
        /// <returns></returns>
        Context CreateBehaviourTreeContext()
        {
            return Context.CreateFromGameObject(gameObject);
        }

        /// <summary>
        /// 選択時に呼び出されるメソッド
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            if (!_behaviorTree)
            {
                return;
            }

            BehaviorTree.Traverse(_behaviorTree.rootNode, (n) =>
            {
                if (n.drawGizmos)
                {
                    n.OnDrawGizmos();
                }
            });
        }
    }
}