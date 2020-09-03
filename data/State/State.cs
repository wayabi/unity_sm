using UnityEngine;
using System.Collections;

public abstract class State<T>
{

    public virtual void OnEnter()
    {
    }

    public virtual void OnExit()
    {
    }

    public abstract void Update();

    private T m_Owner;

    public T Owner
    {
        get{ return m_Owner; }
        set{ m_Owner = value; }
    }

}
