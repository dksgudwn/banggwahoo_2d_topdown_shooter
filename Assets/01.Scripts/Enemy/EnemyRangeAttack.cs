using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : EnemyAttack
{
    [SerializeField]
    private GameObject _enemyBullet;

    [SerializeField]
    private float _coolTime = 3f;

    private float _lastFireTime = 0;
    private FireBall _currentFireBall;

    public override void Attack()
    {
        if (_actionData.IsAttack == false && _lastFireTime + _coolTime < Time.time)
        {
            _actionData.IsAttack = true;

            StartAttackSequence();
        }
    }

    private void StartAttackSequence()
    {
        Sequence seq = DOTween.Sequence();

        _currentFireBall = PoolManager.Instance.Pop("FireBall") as FireBall;
        _currentFireBall.transform.position = transform.position + new Vector3(0, 0.25f, 0);
        _currentFireBall.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        seq.Append(_currentFireBall.transform.DOMoveY(_currentFireBall.transform.position.y + 1f, 0.5f));
        seq.Join(_currentFireBall.transform.DOScale(new Vector3(0.4f, 0.4f, 0.4f), 0.5f)); //위와 아래가 동시에 같이 트윈된다.

        seq.Append(_currentFireBall.transform.DOScale(new Vector3(1f, 1f, 1f), 1.2f)); //위와 아래가 동시에 같이 트윈된다.

        var t = DOTween.To(() => _currentFireBall.Light.intensity,
            value => _currentFireBall.Light.intensity = value,
            _currentFireBall.LightMaxIntensity,
            1.2f
            );
        seq.Join(t);

        seq.AppendCallback(() =>
        {
            _lastFireTime = Time.time;
            _actionData.IsAttack = false;
            _currentFireBall.Fire(_currentFireBall.transform.right * 5f);

            _currentFireBall = null;
        });
    }
    public void FaceDirection(Vector2 pointerInput)
    {
        if (_currentFireBall == null) return;
        Vector3 direction = (Vector3)pointerInput - _currentFireBall.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _currentFireBall.transform.rotation = Quaternion.Euler(0, 0, angle);

        //Vector3 result = Vector3.Cross(Vector2.up, direction);
        //_currentFireBall.Flip(result.z > 0);
    }
}
