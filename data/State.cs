using UnityEngine;
using System.Collections;

public class #NAME#State
{
    public enum State
    {
#STATES#
    }

    public #NAME#State(#NAME#Manager owner)
    {
        //m_StateManager.AddState(State.Init, typeof(AppState_Init), owner);
#ADD_STATE#
    }

    private StateManager<State, #NAME#Manager> m_StateManager = new StateManager<State, #NAME#Manager>();

    public void Change(State state)
    {
        m_StateManager.Change(state);
    }

    public State GetCurrent()
    {
        return m_StateManager.CurrentStateNo;
    }

    public State GetBefore()
    {
        return m_StateManager.BeforeStateNo;
    }

    public void UpdateState()
    {
        m_StateManager.Update();
    }
}
