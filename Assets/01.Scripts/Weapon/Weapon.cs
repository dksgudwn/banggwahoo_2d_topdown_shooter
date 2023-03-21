
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponDataSO _weaponData;
    [SerializeField] protected Transform _muzzle;//�ѱ� ��ġ
    [SerializeField] protected Transform _shellEjectPosition;//ź�� ���� ��ġ

    public WeaponDataSO weaponData => _weaponData;//���߿� ������ �� �� �ְ� ���� ����� �д�

    public UnityEvent OnShoot;
    public UnityEvent OnShootNoAmmo;
    public UnityEvent OnStopShooting;
    protected bool _isShooting; // ���� �߻����ΰ�?
    protected bool _delayCoroutine = false;

    #region AMMO���� �ڵ��
    protected int _ammo;//���� �Ѿ˼�
    public int Ammo
    {
        get { return _ammo; }
        set
        {
            _ammo = Mathf.Clamp(value, 0, _weaponData.ammoCapacity);
        }
    }
    public bool AmmoFull => Ammo == _weaponData.ammoCapacity;
    public int EmpptyBullet => _weaponData.ammoCapacity - _ammo;//���� ������ źȯ��
    #endregion

    private void Awake()
    {
        _ammo = _weaponData.ammoCapacity;//�ִ�ġ�� ó�� ����
    }

    private void Update()
    {
        UseWeapon();
    }

    private void UseWeapon()
    {
        //���⼭ ���� �Ѿ��� �߻��϶�� ����� �԰� �����̰� ���ٸ� �߻�
        if (_isShooting && _delayCoroutine == false)
        {
            //���� �Ѿ��� �ܷ��� �ִ��� üũ�� �ؾ�������
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
                OnShootNoAmmo?.Invoke();
                return;
            }
            FinishOneShooting();//�ѹ� ��� �� �������� ������ �ڷ�ƾ�� ��������ϴϱ� �۾��� ���⼭
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
        SpawnBullet(_muzzle.position, CalculateAngle(_muzzle));
    }


    private Quaternion CalculateAngle(Transform muzzle)
    {
        float spread = Random.Range(-_weaponData.spreadAngle, _weaponData.spreadAngle);
        Quaternion bulletSpreadRot = Quaternion.Euler(new Vector3(0, 0, spread));
        return muzzle.transform.rotation * bulletSpreadRot; //
    }
    private void SpawnBullet(Vector3 position, Quaternion rot)
    {
        RegularBullet b = PoolManager.Instance.Pop("Bullet") as RegularBullet;
        b.SetPositionAndRotation(position, rot);
        b.IsEnemy = false;
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
