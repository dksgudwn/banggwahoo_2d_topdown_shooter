using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    public override void TakeAction()
    {
        //정지시키고 현재 공격중이 아니라면 공격
        _enemyBrain.Move(Vector2.zero, _enemyBrain.Target.position);

        if (_actionData.IsAttack == false)
        {
            _enemyBrain.Attack();
        }
    }
}
