using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public bool IsEnemy { get; set; }

    public Vector3 _hitPoint { get; private set; }

    protected bool _isDead = false;

    [SerializeField]
    protected int _maxHealth;
    protected int _currentHealth;

    public UnityEvent OnGetHit = null; // 맞았을때 발생할 이벤트
    public UnityEvent OnDie = null; // 죽었을때 발생할 이벤트


    private void Awake()
    {
        _currentHealth = _maxHealth;
    }
    public void GetHit(int damage, GameObject damageDealer, Vector3 hitPoint, Vector3 normal)
    {
        if (_isDead) return;

        _currentHealth -= damage;

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
}
