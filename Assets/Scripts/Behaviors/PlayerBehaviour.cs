using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    int damage;
    public float moveSpeed;
    public float cd = 0.5f;
    

    [SerializeField] PlayerController pController;
    
    Rigidbody2D rb2D;

    bool attacking = false;

    public AttackBehaviour curWeapon;

    Animator anim;
    SpriteRenderer sprite;

    // Use this for initialization
    void Start()
    {
        curWeapon = pController.curWeapon;
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoving();
        Attack();
        pController.SetSpell();
    }

    private AttackBehaviour SearchList(AttackBehaviour attackBehaviour)
    {
        foreach(AttackBehaviour attBehaviour in pController.attackList)
        {
            if (attackBehaviour == attBehaviour)
            {
                return attBehaviour;
            }
        }

        return null;
    }
    float previousHMovement = 0;
    private void PlayerMoving()
    {
        if (!attacking)
        {
            float hMovement = Input.GetAxis("Horizontal") * moveSpeed;
            float vMovement = Input.GetAxis("Vertical") * moveSpeed;

            rb2D.velocity = new Vector2(hMovement, vMovement);

            anim.SetFloat("XDirection", hMovement);
            if (hMovement < 0)
            {
                previousHMovement = hMovement;
                //sprite.flipX = true;
                transform.GetChild(0).localScale= new Vector3(-1, 1, 1);
            }
            else if(hMovement > 0)
            {
                previousHMovement = hMovement;
                transform.GetChild(0).localScale= new Vector3(1, 1, 1);
                //sprite.flipX = false;
            }
            else if(hMovement == 0)
            {
                if(previousHMovement < 0)
                {
                    transform.GetChild(0).localScale= new Vector3(-1, 1, 1);
                }
                else 
                {
                    transform.GetChild(0).localScale= new Vector3(1, 1, 1);
                }
            }
            anim.SetFloat("YDirection", vMovement);
            anim.SetFloat("Velocity", rb2D.velocity.magnitude);
        }
    }
    
    public void Attack()
    {
        curWeapon = pController.curWeapon;
        if (curWeapon != null)
        {
            if (Input.GetMouseButtonDown(0) && !SearchList(curWeapon).inCd)
            {
                attacking = true;
                Debug.Log(curWeapon);
                cd = curWeapon.cooldownTime;
                damage = curWeapon.damage;
                SearchList(curWeapon).inCd = true;
                StartCoroutine("AttackCo");
            }
        }
    }

    IEnumerator AttackCo()
    {
        anim.SetInteger("AttackType", curWeapon.anim);
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.2f);
        attacking = false;

        curWeapon.attackCDUI.Key();
        yield return new WaitForSeconds(cd);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponentInParent<EnemyBehavior>().ReceiveDamage(damage, curWeapon.attackType);
        }
    }
}