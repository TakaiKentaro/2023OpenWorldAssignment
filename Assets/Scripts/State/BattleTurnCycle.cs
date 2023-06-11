using UnityEngine;
using State = StateMachine<BattleTurnCycle>.State;

/// <summary>
/// クラス説明
/// </summary>
public class BattleTurnCycle : MonoBehaviour
{
    StateMachine<BattleTurnCycle> stateMachine = null;

    enum EventEnum
    {
        GameStart,
        GameOver,
        ReTry,
    }

    enum TurnEnum
    {
        Player,
        Enemy,
        Action,
    }

    private void Awake()
    {
        stateMachine = new StateMachine<BattleTurnCycle>(this);
    }

    private void Start()
    {
        stateMachine.AddTransition<StartState, InGameState>((int)EventEnum.GameStart);
        stateMachine.AddTransition<InGameState, ResultState>((int)EventEnum.GameOver);
        stateMachine.AddTransition<ResultState, StartState>((int)EventEnum.ReTry);
        
        stateMachine.AddTransition<PlayerState,ActionState>((int)TurnEnum.Player);
        stateMachine.AddTransition<EnemyState, ActionState>((int)TurnEnum.Enemy);
        stateMachine.AddTransition<ActionState,PlayerState>((int)TurnEnum.Action);
        stateMachine.AddTransition<ActionState,EnemyState>((int)TurnEnum.Action);

        stateMachine.Start<StartState>();
    }

    void Update()
    {
        stateMachine.Update();    
    }

    class StartState : State
    {
        protected override void OnEnter(State prevState)
        {
            Debug.Log($"{this}Enter");
        }
        protected override void OnExit(State nextState)
        {
            Debug.Log($"{this}Exit");
        }

        protected override void OnUpdate()
        {
            Debug.Log($"{this}Update");
        }
    }

    class InGameState : State
    {
        protected override void OnEnter(State prevState)
        {
            Debug.Log($"{this}Enter");
        }
        protected override void OnExit(State nextState)
        {
            Debug.Log($"{this}Exit");
        }

        protected override void OnUpdate()
        {
            Debug.Log($"{this}Update");
        }
    }

    class ResultState : State
    {
        protected override void OnEnter(State prevState)
        {
            Debug.Log($"{this}Enter");
        }
        protected override void OnExit(State nextState)
        {
            Debug.Log($"{this}Exit");
        }

        protected override void OnUpdate()
        {
            Debug.Log($"{this}Update");
        }
    }
    
    class PlayerState : State
    {
        protected override void OnEnter(State prevState)
        {
            Debug.Log($"{this}Enter");
        }
        protected override void OnExit(State nextState)
        {
            Debug.Log($"{this}Exit");
        }

        protected override void OnUpdate()
        {
            Debug.Log($"{this}Update");
        }
    }
    
    class EnemyState : State
    {
        protected override void OnEnter(State prevState)
        {
            Debug.Log($"{this}Enter");
        }
        protected override void OnExit(State nextState)
        {
            Debug.Log($"{this}Exit");
        }

        protected override void OnUpdate()
        {
            Debug.Log($"{this}Update");
        }
    }
    
    class ActionState : State
    {
        protected override void OnEnter(State prevState)
        {
            Debug.Log($"{this}Enter");
        }
        protected override void OnExit(State nextState)
        {
            Debug.Log($"{this}Exit");
        }

        protected override void OnUpdate()
        {
            Debug.Log($"{this}Update");
        }
    }

    public void OnStart()
    {
        stateMachine.Dispatch((int)EventEnum.GameStart);
    }
    public void OnInGame()
    {
        stateMachine.Dispatch((int)EventEnum.GameOver);
    }
    public void OnResult()
    {
        stateMachine.Dispatch((int)EventEnum.ReTry);
    }
    public void OnPlayerTurn()
    {
        stateMachine.Dispatch((int)TurnEnum.Player);
    }
    public void OnEnemyTurn()
    {
        stateMachine.Dispatch((int)TurnEnum.Enemy);
    }
    public void OnAction()
    {
        stateMachine.Dispatch((int)TurnEnum.Action);
    }
}