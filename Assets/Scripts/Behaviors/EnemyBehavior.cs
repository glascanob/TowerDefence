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
    public EnemyType enemyType;

    public SpotChecker target;

    public AudioClip walkSound;
    public AudioClip deathSound;
    public AudioClip attackSound;
    public AudioSource source;

    bool isAlive = true;
    protected bool inCD = false;
    protected bool attacking = false;

    protected Animator anim;

    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
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
                    target = TowerController.instance.GetSpot(gameObject, enemyType);
                }
                float newDistance = Vector3.Distance(transform.position, target.position.transform.position);
                if (newDistance > 0.1f)
                {
                    float step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, target.position.transform.position, step);
                }
                else
                {
                    if (target.spotId > 9)
                    {
                        sprite.flipX = true;
                    }
                    else
                    {
                        sprite.flipX = false;
                    }
                    attacking = true;
                }
            }

            if (!attacking)
            {
                source.clip = walkSound;
                if (!source.isPlaying)
                {
                    source.Play();
                }
            }
            else
            {
                source.clip = attackSound;
                if (!source.isPlaying)
                {
                    source.Play();
                }
            }
        }
    }

    public virtual void ReceiveDamage(int damage, AttackTypes attackType)
    {
        if (health > 0)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position);

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