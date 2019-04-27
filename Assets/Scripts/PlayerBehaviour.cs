using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public int maxHealth;
    [SerializeField] private int curHealth;
    public int damage;
    public float moveSpeed;
    //public GameObject weapon;

    Rigidbody2D rb2D;
    Attacks spell = Attacks.sword;
    bool enemyInRange = false;

    // Use this for initialization
    void Start()
    {
        curHealth = maxHealth;
        rb2D = GetComponent<Rigidbody2D>();
        //weapon.SetActive(false); // temporary
    }

    // Update is called once per frame
    void Update()
    {
        if (curHealth > 0)
        {
            PlayerMoving();
            Attack();
            SetSpell();
        }
        else
        {
            Debug.Log("YOU DEAD");
            Destroy(gameObject);
        }
        
    }

    private void PlayerMoving()
    {
        float hMovement = Input.GetAxis("Horizontal") * moveSpeed;
        float vMovement = Input.GetAxis("Vertical") * moveSpeed;

        rb2D.velocity = new Vector2(hMovement, vMovement);
    }

    public void SetSpell()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            Debug.Log("set to Spell 1!!!");
            spell = (Attacks) 1;
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            Debug.Log("set to Spell 2!!!");
            spell =  (Attacks) 2;
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            Debug.Log("set to Spell 3!!!");
            spell = (Attacks) 3;
        } else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("set to Spell 3!!!");
            spell = (Attacks) 4;
        }
    }

    public void SetSpell(int attacks)
    {
        spell = (Attacks)attacks;
    }

    GameObject enemy;
    public void Attack() {
        if (Input.GetMouseButtonDown(0) && enemyInRange) {
            //Enable collider for weapon
            //weapon.SetActive(true); // temporary (after animations are set we can replace this)
            //trigger some animation
            Debug.Log("attacking with " + spell.ToString());
            enemy.GetComponent<EnemyBehavior>().ReceiveDamage(damage);
        }
        else
        {
            //weapon.SetActive(false);
        }
    }

    public void Damage(int damage)
    {
        curHealth -= damage;
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

public enum Attacks
{
    sword, spell1, spell2, spell3, spell4
}

public enum AttackType
{
    heavy, normal, light, air
}
