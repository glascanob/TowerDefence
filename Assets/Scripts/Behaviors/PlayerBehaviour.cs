using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : PlayerController
{
    public int damage;
    public float moveSpeed;
    public Collider2D damageArea;
    public float cd = 0.5f;

    bool inCd = false;
    //public GameObject weapon;

    Rigidbody2D rb2D;
    bool enemyInRange = false;

    // Use this for initialization
    void Start()
    {
        damageArea.enabled = false;
        rb2D = GetComponent<Rigidbody2D>();
        //weapon.SetActive(false); // temporary
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoving();
        Attack();
        SetSpell();
    }

    private void PlayerMoving()
    {
        float hMovement = Input.GetAxis("Horizontal") * moveSpeed;
        float vMovement = Input.GetAxis("Vertical") * moveSpeed;

        rb2D.velocity = new Vector2(hMovement, vMovement);
    }

    GameObject enemy;
    public void Attack()
    {
        if (Input.GetMouseButtonDown(0) && !inCd) {
            Debug.Log("attacking with " + attack.ToString());
            inCd = true;
            StartCoroutine("AttackCo");
        }
        else
        {
            //weapon.SetActive(false);
        }
    }

    IEnumerator AttackCo()
    {
        damageArea.enabled = true;
        yield return new WaitForSeconds(0.2f);
        damageArea.enabled = false;
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
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("Enemy is in Range!");
    //    enemyInRange = true;
    //    enemy = collision.gameObject;
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log("Enemy is out of Range!");
    //    enemyInRange = false;
    //    enemy = null;
    //}
}