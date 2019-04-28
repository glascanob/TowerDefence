using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public static TowerController instance;
    public SceneLoader sceneLoader;

    public int health;
    public GameObject spotParent;
    public GameObject farSpotParent;
    [SerializeField]
    public List<SpotChecker> inerSpots;
    public List<SpotChecker> farSpots;

    public AudioClip damageSound;
    public AudioSource source; 

    [HideInInspector]
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        target = this.transform;
        int ind = 0;
        SpotChecker[] temp = spotParent.GetComponentsInChildren<SpotChecker>();
        foreach(SpotChecker sp in temp)
        {
            sp.spotId = ind;
            inerSpots.Add(sp);
            ind++;
        }
        ind = 0;
        SpotChecker[] temp2 = farSpotParent.GetComponentsInChildren<SpotChecker>();
        foreach (SpotChecker sp in temp2)
        {
            sp.spotId = ind;
            farSpots.Add(sp);
            ind++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public SpotChecker GetSpot(GameObject enemy, EnemyType enemyType)
    {
        SpotChecker spotToSend = new SpotChecker();
        float distance = 0;
        float tempDistance = 0;
        switch(enemyType)
        {
            case EnemyType.enemy1:
                spotToSend = inerSpots[0];
                distance = Vector3.Distance(enemy.transform.position, spotToSend.position.transform.position);
                for (int i = 1; i < inerSpots.Count; i++)
                {
                    if (inerSpots[i].enemyQuantity < spotToSend.enemyQuantity)
                    {
                        spotToSend = inerSpots[i];
                    }
                    else if (inerSpots[i].enemyQuantity == spotToSend.enemyQuantity)
                    {
                        tempDistance = Vector3.Distance(enemy.transform.position, inerSpots[i].position.transform.position);
                        if (tempDistance < distance)
                        {
                            spotToSend = inerSpots[i];
                        }
                    }
                    distance = Vector3.Distance(enemy.transform.position, spotToSend.position.transform.position);
                }
                break;
            case EnemyType.enemy2:
                spotToSend = farSpots[0];
                distance = Vector3.Distance(enemy.transform.position, spotToSend.position.transform.position);
                for (int i = 1; i < farSpots.Count; i++)
                {
                    if (farSpots[i].enemyQuantity < spotToSend.enemyQuantity)
                    {
                        spotToSend = farSpots[i];
                    }
                    else if (farSpots[i].enemyQuantity == spotToSend.enemyQuantity)
                    {
                        tempDistance = Vector3.Distance(enemy.transform.position, farSpots[i].position.transform.position);
                        if (tempDistance < distance)
                        {
                            spotToSend = farSpots[i];
                        }
                    }
                    distance = Vector3.Distance(enemy.transform.position, spotToSend.position.transform.position);
                }
                break;
        }
        spotToSend.enemyQuantity++;
        return spotToSend;
    }

    public void ReceiveDamage(int damage)
    {
        source.clip = damageSound;
        if (!source.isPlaying)
        {
            source.Play();
        }
        health -= damage;

        if (health <= 0)
        {
            SceneLoader.instance.LoadNextScene();
        }
    }
}