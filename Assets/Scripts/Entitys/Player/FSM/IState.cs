﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public enum StateType 
    {
        Idle,
        Jump,
        Dash,
        Move,

        FireMagic
    }
    public interface IState
    {
        void OnEnter() { }
        void UpdateAction() { }
        void OnExit() { }
    }



