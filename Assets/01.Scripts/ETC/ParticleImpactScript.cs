using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleImpactScript : ImpactScript
{
    private ParticleSystem[] _particles;

    protected override void Awake()
    {
        base.Awake();
        _particles = GetComponentsInChildren<ParticleSystem>();
    }

    public override void SetPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        base.SetPositionAndRotation(pos, rot);
        StartCoroutine(DisableCoroutine());
    }
    protected IEnumerator DisableCoroutine()
    {
        foreach (var p in _particles)
        {
            p.Play();
        }
        yield return new WaitForSeconds(2f);
        DestroyAfterAnimation();
    }
}
