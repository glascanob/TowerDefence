using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    public AttackCDUI attackCDUI;

    public AttackTypes attackType;
    public int damage;
    public Animator anim;
    public float cooldownTime;
}
