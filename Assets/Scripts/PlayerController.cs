using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public List<AttackBehaviour> attackList;
    public AttackBehaviour curWeapon;
    private void Start()
    {
        curWeapon = attackList[0];
    }

    public void SetSpell()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("0");
            curWeapon = attackList[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("1");
            curWeapon = attackList[1];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("2");
            curWeapon = attackList[2];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("3");
            curWeapon = attackList[3];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("4");
            curWeapon = attackList[4];
        }
    }

    public void SetSpell(int attacks)
    {
        curWeapon = attackList[attacks];
    }
}
