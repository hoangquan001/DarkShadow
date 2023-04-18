using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkShadow
{
    public class StateMachine
    {
        public Dictionary<int, IState> DirStates = null;

        IState currentState;

        public StateMachine()
        {
            Init();
        }
        void Init()
        {
            currentState = null;
        }

        public void addState(int stateId, IState state)
        {
            DirStates[stateId] = state;
        }
        public void setCurrentState(int stateId)
        {
            currentState = DirStates[stateId];
        }

        public void TransitionTo(int nextStateId)
        {
            currentState.OnExit();
            currentState = DirStates[nextStateId];
            currentState.OnEnter();
        }

        public void Update()
        {
            currentState.Update();
        }
        public void FixedUpdate()
        {
            currentState.FixedUpdate();
        }

    }
}
