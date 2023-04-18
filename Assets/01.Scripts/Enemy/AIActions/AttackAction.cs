using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    public override void TakeAction()
    {
        //������Ű�� ���� �������� �ƴ϶�� ����
        _enemyBrain.Move(Vector2.zero, _enemyBrain.Target.position);

        if (_actionData.IsAttack == false)
        {
            _enemyBrain.Attack();
        }
    }
}
