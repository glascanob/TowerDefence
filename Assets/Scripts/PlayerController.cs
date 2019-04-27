using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected Attacks attack = Attacks.sword;

    protected void SetSpell()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("set to Spell 1!!!");
            attack = (Attacks)1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("set to Spell 2!!!");
            attack = (Attacks)2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("set to Spell 3!!!");
            attack = (Attacks)3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("set to Spell 3!!!");
            attack = (Attacks)4;
        }
    }

    public void SetSpell(int attacks)
    {
        attack = (Attacks)attacks;
    }
}

public enum Attacks
{
    sword, spell1, spell2, spell3, spell4
}

public enum AttackType
{
    heavy, normal, light, air
}
