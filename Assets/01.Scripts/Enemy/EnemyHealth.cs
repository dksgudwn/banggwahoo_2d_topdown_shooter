using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public bool IsEnemy { get; set; }
    public Vector3 _hitPoint { get; private set; }

    protected bool _isDead = false; //��� ���θ� ��Ÿ���� ��

    [SerializeField]
    protected int _maxHealth; //�̰� ���߿� SO�� ���Ե� �Ŵ�.

    protected int _currentHealth;

    public UnityEvent OnGetHit = null;  //�¾��� �� �߻��� �̺�Ʈ��
    public UnityEvent OnDie = null;  // �׾��� �� �߻��� �̺�Ʈ

    private AIActionData _AIActionData;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _AIActionData = transform.Find("AI").GetComponent<AIActionData>();
    }

    public void GetHit(int damage, GameObject damageDealer, Vector3 hitPoint, Vector3 normal)
    {
        if (_isDead) return;

        Debug.Log(damage);
        _currentHealth -= damage;

        _AIActionData.hitPoint = hitPoint;
        _AIActionData.hitNormal = normal;

        OnGetHit?.Invoke();

        if (_currentHealth <= 0)
        {
            DeadProcess();
        }
    }

    private void DeadProcess()
    {
        _isDead = true;
        OnDie?.Invoke();
    }
    public void Reset()
    {
        _isDead = false;
    }
}
