using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.Universal.Internal;

public class FireBall : PoolableMono
{
    private Light2D _light;
    public Light2D Light => _light;
    public float LightMaxIntensity = 2.5f;

    private bool _isDead = false;
    private Rigidbody2D _rigid;
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private LayerMask _whatIsEnemy;
    [SerializeField]
    private BulletDataSO _bulletData;
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
        _rigid.velocity = direction * _bulletData.bulletSpeed;
    }

    public override void Init()
    {
        _light.intensity = 0;
        transform.localScale = Vector3.one;
        _rigid.velocity = Vector3.zero;
        _isDead = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle") ||
            ((1 << collision.gameObject.layer) & _whatIsEnemy) > 0)
        {
            HitObstacle(collision);
            _isDead = true;
            PoolManager.Instance.Push(this);
        }
    }
    private void HitObstacle(Collider2D collision)
    {
        ImpactScript impact = PoolManager.Instance.Pop(_bulletData.impactObstaclePrefab.name) as ImpactScript;


        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f)));
        Vector3 explosionPosition = transform.position * 0.5f;

        impact.SetPositionAndRotation(explosionPosition, rot); //요것도 약간 어색할거다.

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2.5f, _whatIsEnemy);

        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out IDamageable health))
            {
                Vector3 normal = (transform.position - collider.transform.position).normalized;
                health.GetHit(_bulletData.damage, gameObject, collider.transform.position, normal);
            }
        }
    }
}
