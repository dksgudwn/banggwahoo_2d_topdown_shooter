using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactScript : PoolableMono
{
    protected AudioSource _audioSource;

    protected virtual void Awake()
    {
        _audioSource = GetComponent<AudioSource>(); 
    }

    public virtual void DestroyAfterAnimation()
    {
        PoolManager.Instance.Push(this);
    }

    public override void Init()
    {
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
    }

    public virtual void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
        if(_audioSource != null && _audioSource.clip != null)
        {
            _audioSource.Play();
        }
    }

    public virtual void SetLocalScale(Vector3 scale)
    {
        transform.localScale = scale;
    }
}
