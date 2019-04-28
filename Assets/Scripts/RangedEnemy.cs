using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : EnemyBehavior
{
    public float cDBetweenAttacks;

    public GameObject projectilPrefab;

    public override void Update()
    {
        base.Update();

        if(attacking)
        {
            if(!inCD)
            {
                inCD = true;
                AttackTower();
            }
        }
    }

    public void AttackTower()
    {
        anim.SetTrigger("Attack");

        Instantiate(projectilPrefab, transform.position, Quaternion.identity);

        StartCoroutine(base.Attack(cDBetweenAttacks));
    }

    public override void ReceiveDamage(int damage, AttackTypes attackType)
    {
        if (attackType == AttackTypes.light)
        {
            base.ReceiveDamage(damage, attackType);
        }
    }
}
