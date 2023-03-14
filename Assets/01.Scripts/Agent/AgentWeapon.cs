using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    private float _desireAngle;//무기가 바라봐야 하는 방향

    protected WeaponRenderer _weaponRenderer;
    protected Weapon _weapon;

    protected virtual void Awake()
    {
        _weaponRenderer = GetComponentInChildren<WeaponRenderer>();
        _weapon = GetComponentInChildren<Weapon>();//자기가 들고 있는 총 가져오기
    }

    public virtual void AimWeapon(Vector2 pointerPos)
    {
        Vector3 aimDirection = (Vector3)pointerPos - transform.position;//마우스 방향 벡터를 구하고

        _desireAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        //디그리 각도를 구한다.

        AdjustWeaponRendering();

        transform.rotation = Quaternion.AngleAxis(_desireAngle, Vector3.forward);
        //z축 기준으로 회전
    }

    private void AdjustWeaponRendering()
    {
        if (_weaponRenderer != null)
        {
            _weaponRenderer.FlipSprite(_desireAngle > 90f || _desireAngle < -90f);
            _weaponRenderer.RenderBehindHead(_desireAngle > 0 && _desireAngle < 180);
        }
    }

    public virtual void Shoot()
    {
        //무기를 들고있을경우 발사
        _weapon?.TryShooting();
    }

    public virtual void StopShooting()
    {
        _weapon?.StopShooting();
    }
}
