using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    int damage;
    public float moveSpeed;
    public Collider2D damageArea;
    public float cd = 0.5f;

    public float energyBoostFactor = 5f;
    public int energyBoostAmount;
    public float energyTime = 1f;

    public AudioClip walkSound;
    public AudioClip attackSound;
    public AudioSource source;
    

    [SerializeField] PlayerController pController;
    
    Rigidbody2D rb2D;
    bool enemyInRange = false;

    public AttackBehaviour curWeapon;

    Animator anim;
    SpriteRenderer sprite;

    // Use this for initialization
    void Start()
    {
        curWeapon = pController.curWeapon;
        damageArea.enabled = false;
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
        EnergyBoost();
    }

    float timer = 0;
    bool key = false;
    private void EnergyBoost()
    {
        if (Input.GetKeyDown(KeyCode.Space) && energyBoostAmount > 0)
        {
            energyBoostAmount--;
            moveSpeed += energyBoostFactor;
            timer += energyTime;
            key = true;
        }
        if (key)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else if (timer <= 0)
            {
                moveSpeed -= energyBoostFactor;
                key = false;
            }
        }
        
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

    private void PlayerMoving()
    {
        float hMovement = Input.GetAxis("Horizontal") * moveSpeed;
        float vMovement = Input.GetAxis("Vertical") * moveSpeed;

        if (hMovement != 0 || vMovement != 0)
        {
            source.clip = walkSound;
            if(!source.isPlaying)
                source.Play();
        }

        rb2D.velocity = new Vector2(hMovement, vMovement);

        anim.SetFloat("XDirection", hMovement);
        if(hMovement < 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
        anim.SetFloat("YDirection", vMovement);
        anim.SetFloat("Velocity", rb2D.velocity.magnitude);
    }
    
    public void Attack()
    {
        curWeapon = pController.curWeapon;
        if (curWeapon != null)
        {
            if (Input.GetMouseButtonDown(0) && !SearchList(curWeapon).inCd)
            {
                source.clip = attackSound;
                source.Play();
                cd = curWeapon.cooldownTime;
                damage = curWeapon.damage;
                SearchList(curWeapon).inCd = true;
                StartCoroutine("AttackCo");
            }
        }
    }

    IEnumerator AttackCo()
    {
        damageArea.enabled = true;
        yield return new WaitForSeconds(0.2f);
        damageArea.enabled = false;

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