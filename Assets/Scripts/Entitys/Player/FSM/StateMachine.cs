
using System.Collections.Generic;
using DarkShadow;
using UnityEngine;


public class StateMachine
{
    XEntity entity;
    IState curState;
    IState defaultState;
    public float startTime = 0;

    public StateType CurStateType
    {
        get
        {
            return curState.stateId;
        }
    }
    public IState CurState
    {
        get
        {
            return curState;
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

    // public void addState(StateType stateId, IState state)
    // {
    //     DirStates[stateId] = state;
    // }
    public void SetDefaultState(IState State)
    {
        defaultState = State;
        curState = defaultState;
        curState.OnEnter();
    }

    public void TransitionTo(IState State)
    {
        curState.OnExit();
        curState = State;
        curState.OnEnter();
        startTime = Time.time;
    }

    public void Update()
    {
        curState.UpdateAction();
    }
    public void FixedUpdate()
    {
        curState.FixedUpdateAction();
    }

}
