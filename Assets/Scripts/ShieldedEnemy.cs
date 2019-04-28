using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldedEnemy : EnemyBehavior
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
        if (attackType == AttackTypes.heavy)
        {
            base.ReceiveDamage(damage, attackType);
        }
    }
}
