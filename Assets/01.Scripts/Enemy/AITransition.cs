using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    public List<AIDecision> decisions;

    public AIState TransitionState;

    public void SetUp(Transform parentTrm)
    {
        decisions.ForEach(d => d.Setup(parentTrm));
    }
    public bool CanTransition()
    {
        bool result = false;
        foreach (AIDecision d in decisions)
        {
            result = d.MakeADecision();
            if (d.IsReverse)
                result = !result;

            if (result == false) break;


        }
        return result;
    }
}
