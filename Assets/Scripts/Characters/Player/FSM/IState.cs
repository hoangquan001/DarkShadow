using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarkShadow
{
    public class IState
    {
        public virtual void OnEnter() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void OnExit() { }

    }



    public class IdleState : IState
    {
        PlayerController controller;
        public IdleState(PlayerController controller)
        {

        }
        public void OnEnter()
        {

        }
        public void Update()
        {

        }
        public void FixedUpdate()
        {

        }
        public void OnExit()
        {

        }

    }
    public class JumpState : IState
    {
        PlayerController controller;
        public JumpState(PlayerController controller)
        {

        }
        public void OnEnter()
        {

        }
        public void Update()
        {

        }
        public void FixedUpdate()
        {

        }
        public void OnExit()
        {

        }

    }
    public class DashState : IState
    {
        PlayerController controller;
        public DashState(PlayerController controller)
        {

        }
        public void OnEnter()
        {

        }
        public void FixedUpdate()
        {

        }
        public void Update()
        {

        }
        public void OnExit()
        {

        }

    }
    public class MoveState : IState
    {
        PlayerController controller;
        public MoveState(PlayerController controller)
        {

        }
        public void OnEnter()
        {

        }
        public void Update()
        {

        }

        public void FixedUpdate()
        {

        }
        public void OnExit()
        {

        }

    }
}
