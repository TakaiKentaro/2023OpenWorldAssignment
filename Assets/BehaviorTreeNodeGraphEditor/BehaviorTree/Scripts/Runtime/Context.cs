using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace BehaviorTreeNodeGraphEditor
{
    /// <summary>
    /// 共有されるオブジェクトであり、すべてのノードがアクセスできるコンテキスト
    /// よく使用されるコンポーネントやサブシステムはここに格納
    /// どのようなものを追加するかは、ゲームによって異なる
    /// 必要に応じてこのクラスを拡張する
    /// </summary>
    public class Context
    {
        public GameObject gameObject;
        public Transform transform;
        public Animator animator;
        public Rigidbody physics;
        public NavMeshAgent agent;
        public SphereCollider sphereCollider;
        public BoxCollider boxCollider;
        public CapsuleCollider capsuleCollider;

        public CharacterController characterController;
        // ゲーム固有のシステムをここに追加する

        public static Context CreateFromGameObject(GameObject gameObject)
        {
            // 一般的に使用されるコンポーネントを取得
            Context context = new Context();
            context.gameObject = gameObject;
            context.transform = gameObject.transform;
            context.animator = gameObject.GetComponent<Animator>();
            context.physics = gameObject.GetComponent<Rigidbody>();
            context.agent = gameObject.GetComponent<NavMeshAgent>();
            context.sphereCollider = gameObject.GetComponent<SphereCollider>();
            context.boxCollider = gameObject.GetComponent<BoxCollider>();
            context.capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
            context.characterController = gameObject.GetComponent<CharacterController>();

            // 必要なものをここに追加
            
            return context;
        }
    }
}