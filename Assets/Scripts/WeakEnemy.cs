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
}
