using System;
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

    Fall,

    FireMagic
}
public interface IState
{
    StateType stateId { get; }
    void OnEnter();
    void UpdateAction();
    void OnExit();
}



