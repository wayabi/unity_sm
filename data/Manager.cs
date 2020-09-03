using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class #NAME#Manager : MonoBehaviour
{
    public #NAME#State state { get; private set; }

    void Start()
    {
        state = new #NAME#State(this);
        state.Change(#NAME#State.State.Init);
    }

    void Update()
    {
        state.UpdateState();
    }
}
