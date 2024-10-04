
using System.Collections.Generic;
using DarkShadow;


public class StateMachine
    {
        public Dictionary<StateType, IState> DirStates = new Dictionary<StateType, IState>();
        IState currentState;


        private int [,] StateMap  = null;

        public StateMachine()
        {
            Init();
        }
        public void  Init()
        {
            currentState = null;
        }

        public void addState(StateType stateId, IState state)
        {
            DirStates[stateId] = state;
        }
        public void setCurrentState(StateType stateId)
        {
            currentState = DirStates[stateId];
        }

        public void TransitionTo(StateType nextStateId)
        {
            currentState.OnExit();
            currentState = DirStates[nextStateId];
            currentState.OnEnter();
        }

        public void Update()
        {
            currentState.UpdateAction();
        }

    }
