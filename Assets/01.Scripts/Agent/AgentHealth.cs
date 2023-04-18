using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int _maxHP;
    private int _currentHP;

    public UnityEvent<int> OnInitHealth = null;
    public UnityEvent<int, int> OnHealthChanged = null;

    public int Health
    {
        get => _currentHP;
        set
        {
            _currentHP = value;
            _currentHP = Mathf.Clamp(_currentHP, 0, _maxHP);
        }
    }
    [SerializeField]
    private bool _isDead = false;
    public bool IsEnemy => false;



    public Vector3 _hitPoint { get; set; }

    public UnityEvent OnGetHit = null;
    public UnityEvent OnDead = null;

    private void Start()
    {
        _currentHP = _maxHP;
        _isDead = false;
        OnInitHealth?.Invoke(_maxHP);
        OnHealthChanged?.Invoke(_currentHP, _maxHP);
    }

    public void AddHealth(int value)
    {
        Health += value;
        OnHealthChanged?.Invoke(_currentHP, _maxHP);
    }
    public void GetHit(int damage, GameObject damageDealer, Vector3 hitPoint, Vector3 normal)
    {
        Debug.Log(_currentHP);
        if (_isDead) return;

        Health -= damage;
        OnGetHit?.Invoke();
        if (Health <= 0)
        {
            OnDead?.Invoke();
            _isDead = true;
        }
        OnHealthChanged?.Invoke(_currentHP, _maxHP);
    }
}
