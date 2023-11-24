
using System.Collections.Generic;

namespace DarkShadow
{
    public class StateMachine : XComponent
    {
        public Dictionary<StateType, IState> DirStates = new Dictionary<StateType, IState>();

        IState currentState;

        private int [,] StateMap  = null;

        // public StateMachine()
        // {
        //     Init();
        // }
        public override void  Init()
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

        public override void Update()
        {
            currentState.Update();
        }
        public override void  FixedUpdate()
        {
            currentState.FixedUpdate();
        }

    }
}
