using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponDataSO _weaponData;
    [SerializeField] protected Transform _muzzle;//총구 위치
    [SerializeField] protected Transform _shellEjectPosition;//탄피 배출 위치

    public WeaponDataSO weaponData => _weaponData;//나중에 가져다 쓸 수 있게 겟터 만들어 둔다

    public UnityEvent OnShoot;
    public UnityEvent OnShootNoAmmon;
    public UnityEvent OnStopShooting;
    protected bool _isShooting; // 현재 발사중인가?
    protected bool _delayCoroutine = false;

    #region AMMO관련 코드들
    protected int _ammo;//현재 총알수
    public int Ammo
    {
        get { return _ammo; }
        set
        {
            _ammo = Mathf.Clamp(value, 0, _weaponData.ammoCapacity);
        }
    }
    public bool AmmoFull => Ammo == _weaponData.ammoCapacity;
    public int EmpptyBullet => _weaponData.ammoCapacity - _ammo;//현재 부족한 탄환수
    #endregion

    private void Awake()
    {
        _ammo = _weaponData.ammoCapacity;//최대치로 처음 셋팅
    }

    private void Update()
    {
        UseWeapon();
    }

    private void UseWeapon()
    {
        //여기서 만약 총알을 발사하라고 명령이 왔고 딜레이가 없다면 발사
        if (_isShooting && _delayCoroutine == false)
        {
            //현재 총알의 잔량이 있는지 체크를 해야하지만
            if (Ammo > 0)
            {
                OnShoot?.Invoke();
                for (int i = 0; i < _weaponData.bulletcount; i++)
                {
                    ShootBullet();
                }
            }
            else
            {
                _isShooting = false;
                OnShootNoAmmon?.Invoke();
                return;
            }
            FinishOneShooting();//한발 쏘고 난 다음에는 딜레이 코루틴을 돌려줘야하니까 작업을 여기서
        }
    }

    private void FinishOneShooting()
    {
        StartCoroutine(DelayNextShootCoroutine());
        if (_weaponData.autoFire == false)
        {
            _isShooting = false;
        }
    }

    private IEnumerator DelayNextShootCoroutine()
    {
        _delayCoroutine = true;
        yield return new WaitForSeconds(_weaponData.weaponDelay);
        _delayCoroutine = false;
    }

    private void ShootBullet()
    {
        Debug.Log("토끼가 총총총총총");
    }

    public void TryShooting()
    {
        _isShooting = true;
    }

    public void StopShooting()
    {
        _isShooting = false;
        OnStopShooting?.Invoke();
    }
}
