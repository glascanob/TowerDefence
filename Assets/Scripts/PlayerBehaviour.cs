using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : PlayerController
{
    public int damage;
    public float moveSpeed;
    //public GameObject weapon;

    Rigidbody2D rb2D;
    bool enemyInRange = false;

    // Use this for initialization
    void Start()
    {
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
        if (Input.GetMouseButtonDown(0) && enemyInRange) {
            Debug.Log("attacking with " + attack.ToString());
            enemy.GetComponent<EnemyBehavior>().ReceiveDamage(damage);
        }
        else
        {
            //weapon.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enemy is in Range!");
        enemyInRange = true;
        enemy = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Enemy is out of Range!");
        enemyInRange = false;
        enemy = null;
    }
}