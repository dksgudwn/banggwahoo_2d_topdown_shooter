using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    public List<AIDecision> decisions;

    public AIState TransitionState; //전이할 상태

    private void Awake()
    {
        GetComponents<AIDecision>(decisions);
    }
    public void Setup(Transform parentTrm)
    {
        decisions.ForEach(d => d.SetUp(parentTrm));
    }

    public bool CanTransition()
    {
        bool result = false;
        foreach (AIDecision d in decisions)
        {
            result = d.MakeADecision();
            if (d.IsReverse)
                result = !result;

            if (result == false)
                break;
        }

        return result;
    }
}
