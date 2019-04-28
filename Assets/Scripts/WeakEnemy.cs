using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakEnemy : EnemyBehavior
{
    public float cDBetweenAttacks;
    public override void Update()
    {
        base.Update();

        if(attacking)
        {
            if(!inCD)
            {
                inCD = true;
                StartCoroutine(base.Attack(cDBetweenAttacks));
            }
        }
    }
    public override void ReceiveDamage(int damage, AttackTypes attackType)
    {
        if (attackType == AttackTypes.air || attackType == AttackTypes.spawn)
            return;
        base.ReceiveDamage(damage, attackType);
    }
}
