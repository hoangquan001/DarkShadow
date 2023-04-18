using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DarkShadow
{
    public class BossIdleState : IState
    {
        BossController controller;
        public BossIdleState(BossController controller)
        {

        }
        public void OnEnter()
        {

        }
        public void Update()
        {

        }

        public void OnExit()
        {

        }

    }
    public class BossJumpAttackState : IState
    {
        PlayerController controller;
        public BossJumpAttackState(PlayerController controller)
        {

        }
        public void OnEnter()
        {

        }
        public void Update()
        {

        }


        public void OnExit()
        {

        }

    }


    public class BossSpinAttackState : IState
    {
        BossController controller;
        public BossSpinAttackState(BossController controller)
        {
            this.controller = controller;
        }
        public void OnEnter()
        {

        }
        public void Update()
        {
            controller.OnSpinAtack();
        }
        public void OnExit()
        {

        }

    }

}
