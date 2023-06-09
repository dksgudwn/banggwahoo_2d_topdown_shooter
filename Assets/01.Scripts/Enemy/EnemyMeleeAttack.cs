using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAttack
{
    public override void Attack()
    {
        if (_actionData.IsAttack == false)
        {

            if (_brain.Target.TryGetComponent<IDamageable>(out IDamageable health))
            {
                _actionData.IsAttack = true;
                AttackFeedback?.Invoke();
                Vector3 normal = (_brain.transform.position - _brain.Target.position).normalized;
                health.GetHit(_damage, _brain.gameObject, _brain.Target.position, normal);

            }
            StartCoroutine(WaitBeforeCooltime());
        }
    }
}
