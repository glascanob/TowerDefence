using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveTNT : MonoBehaviour
{
    public float timer = 3f;

    Animator anim;

    [SerializeField]
    List<GameObject> enemiesInRange;
    bool key = false;
    CircleCollider2D col;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        enemiesInRange = new List<GameObject>();
        key = true;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (key)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                foreach (GameObject enemy in enemiesInRange)
                    {
                        Debug.Log("I will kil " + enemy);
                        enemy.GetComponentInParent<EnemyBehavior>().ReceiveDamage(5, AttackTypes.spawn);
                    }
                anim.SetBool("Explode", true);
                Destroy(gameObject, 0.5f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enemiesInRange.Contains(collision.gameObject) && collision.CompareTag("Enemy"))
        {
            enemiesInRange.Add(collision.gameObject);
        }
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enemiesInRange.Contains(collision.gameObject) && collision.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(collision.gameObject);
        }
    }
    
}
