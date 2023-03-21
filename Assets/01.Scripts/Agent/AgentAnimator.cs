using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentAnimator : MonoBehaviour
{
    protected Animator _animator;
    protected readonly int _walkBoolHash = Animator.StringToHash("Walk");

    public UnityEvent OnFootStep = null;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void AnimatePlayer(float velocity)
    {
        SetwalkAnimation(velocity > 0);
    }

    public void SetwalkAnimation(bool value)
    {
        _animator.SetBool(_walkBoolHash, value);
    }

    public void FootStepEvent()
    {
        OnFootStep?.Invoke();
    }
}
