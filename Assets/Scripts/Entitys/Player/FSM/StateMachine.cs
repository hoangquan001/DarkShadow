
using System.Collections.Generic;
using DarkShadow;


public class StateMachine
{
    XEntity entity;
    public Dictionary<StateType, IState> DirStates = new Dictionary<StateType, IState>();
    IState curState;
    IState defaultState;

    public StateType CurrentState
    {
        get
        {
            return curState.stateId;
        }
    }

    private int[,] StateMap = null;

    public StateMachine(XEntity entity)
    {
        this.entity = entity;
        Init();
    }
    public void Init()
    {
        curState = null;
    }

    public void addState(StateType stateId, IState state)
    {
        DirStates[stateId] = state;
    }
    public void SetDefaultState(StateType stateId)
    {
        defaultState = DirStates[stateId];
        curState = defaultState;
    }

    public void TransitionTo(StateType nextStateId)
    {
        curState.OnExit();
        curState = DirStates[nextStateId];
        curState.OnEnter();
    }

    public void Update()
    {
        curState.UpdateAction();
    }

}
