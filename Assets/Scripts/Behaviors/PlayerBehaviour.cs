using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public int damage;
    public float moveSpeed;
    public Collider2D damageArea;
    public float cd = 0.5f;

    [SerializeField] PlayerController pController;

    bool inCd = false;
    Rigidbody2D rb2D;
    bool enemyInRange = false;

    AttackBehaviour curWeapon;

    // Use this for initialization
    void Start()
    {
        damageArea.enabled = false;
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoving();
        Attack();
    }

    private void PlayerMoving()
    {
        float hMovement = Input.GetAxis("Horizontal") * moveSpeed;
        float vMovement = Input.GetAxis("Vertical") * moveSpeed;

        rb2D.velocity = new Vector2(hMovement, vMovement);
    }
    
    public void Attack()
    {
        if (Input.GetMouseButtonDown(0) && !inCd) {
            curWeapon = pController.curWeapon;

            damage = curWeapon.damage;
            cd = curWeapon.cooldownTime;

            inCd = true;
            StartCoroutine("AttackCo");
        }
    }

    IEnumerator AttackCo()
    {
        damageArea.enabled = true;
        yield return new WaitForSeconds(0.2f);
        damageArea.enabled = false;

        curWeapon.attackCDUI.Key();
        yield return new WaitForSeconds(cd);
        inCd = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponentInParent<EnemyBehavior>().ReceiveDamage(damage);
        }
    }
}