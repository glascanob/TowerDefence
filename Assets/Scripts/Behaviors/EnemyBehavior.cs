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

    public List<AudioClip> walkSound;
    public List<AudioClip> attackSound;
    public AudioClip deathSound;
    public AudioSource source;

    public GameObject energyDrop;
    public float dropRate = 0.5f;

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
                    if (target.spotId > TowerController.instance.splitIn)
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
                if (!source.isPlaying)
                {
                    source.clip = walkSound[Random.Range(0, walkSound.Count)];
                    source.Play();
                }
            }
            else
            {
                if (!source.isPlaying)
                {
                    source.clip = attackSound[Random.Range(0, attackSound.Count)];
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

            if (Random.Range(0.0f, 1.0f) >= dropRate)
            {
                Instantiate(energyDrop, transform.position, Quaternion.identity);
            }

            isAlive = health - damage <= 0 ? false : true;
            health -= damage;
            if (!isAlive) {
                //ScoreController.instance.AddScore(scoreValue);
                SpawnController.instance.KillEnemy(index);
            }
        }
    }

    protected IEnumerator Attack(float cd)
    {
        TowerController.instance.ReceiveDamage(damage);
        yield return new WaitForSeconds(cd);
        inCD = false;
    }
}