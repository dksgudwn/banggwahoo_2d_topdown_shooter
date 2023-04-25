using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireBall : PoolableMono
{
    private Light2D _light;
    public Light2D Light => _light;
    public float LightMaxIntensity = 2.5f;

    private Rigidbody2D _rigid;
    private SpriteRenderer _spriteRenderer;
    private void Awake()
    {
        _light = transform.Find("Light 2D").GetComponent<Light2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void Flip(bool value)
    {
        _spriteRenderer.flipX = value;
    }

    public void Fire(Vector2 direction)
    {
        _rigid.velocity = direction;
    }

    public override void Init()
    {
        _light.intensity = 0;
        transform.localScale = Vector3.one;
        _rigid.velocity = Vector3.zero;
    }
}
