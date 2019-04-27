using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int index;
    public int health;
    public int damage;
    public float speed;
    public float distance;

    bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        float distToTower = Vector3.Distance(transform.position, TowerController.instance.target.position);
        if (isAlive && distToTower > distance)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, TowerController.instance.target.position, step);
        }
    }

    public void ReceiveDamage(int damage)
    {
        if (health > 0)
        {
            isAlive = health - damage <= 0 ? false : true;
            health -= damage;
            if (!isAlive)
                SpawnController.instance.KillEnemy(index);
        }
    }
}
