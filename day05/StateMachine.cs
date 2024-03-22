using System;
using System.Collections.Generic;

public class StateMachine
{
    public enum State
    {
        Init,
        Idle,
        Map,
        Collect,
    }

    public State state;

    public State GetState()
    {
        return state;
    }

    public void setStateFromLine(string line)
    {
        if (line.Length == 0)
        {
            state = State.Collect;
            return;
        } else if (line.Contains("seeds"))
        {
            state = State.Init;
        } else if (state == State.Collect)
        {
            state = State.Idle;
        } else if (state == State.Idle)
        {
            state = State.Map;
        }
    }
}

