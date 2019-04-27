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

    public SpotChecker target;

    bool isAlive = true;
    protected bool inCD = false;
    protected bool attacking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        float distToTower = Vector3.Distance(transform.position, TowerController.instance.target.position);
        if (isAlive)
        {
            if (distToTower > distance)
            {
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, TowerController.instance.target.position, step);
            }
            else
            {
                if (target == null)
                {
                    target = TowerController.instance.GetSpot(gameObject);
                }
                float newDistance = Vector3.Distance(transform.position, target.position.transform.position);
                if(newDistance > 0.1f)
                {
                    float step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, target.position.transform.position, step);
                }
                else
                {
                    attacking = true;
                }
            }
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

    protected IEnumerator Attack(float cd)
    {
        TowerController.instance.ReceiveDamage(damage);
        yield return new WaitForSeconds(cd);
        inCD = false;
    }
}
