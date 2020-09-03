using UnityEngine;
using System.Collections;

public class #NAME#State_#STATE# : State<#NAME#Manager>
{
    public override void OnEnter()
    {
        Owner.StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {
        //yield return new WaitForSeconds(1f);
        //Owner.state.Change(#NAME#State.State.);
        yield break;
    }

    public override void OnExit()
    {
    }

    public override void Update()
    {
        
    }
}
