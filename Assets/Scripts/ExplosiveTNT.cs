using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveTNT : MonoBehaviour
{
    public float timer = 3f;

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
                if (enemiesInRange != null)
                {
                    col.enabled = false;
                    foreach (GameObject enemy in enemiesInRange)
                    {
                        enemy.GetComponent<EnemyBehavior>().ReceiveDamage(5, AttackTypes.spawn);
                    }
                }
                
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enemiesInRange.Contains(collision.gameObject) && !collision.CompareTag("Player"))
        {
            enemiesInRange.Add(collision.gameObject);
        }
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enemiesInRange.Contains(collision.gameObject))
        {
            enemiesInRange.Remove(collision.gameObject);
        }
    }
    
}
