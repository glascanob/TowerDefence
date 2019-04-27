using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public static TowerController instance;

    public int health;
    public GameObject spotParent;
    [SerializeField]
    public List<SpotChecker> inerSpots;

    [HideInInspector]
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        target = this.transform;

        SpotChecker[] temp = spotParent.GetComponentsInChildren<SpotChecker>();
        foreach(SpotChecker sp in temp)
        {
            inerSpots.Add(sp);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public SpotChecker GetSpot(GameObject enemy)
    {
        SpotChecker spotToSend = inerSpots[0];
        float distance = Vector3.Distance(enemy.transform.position, spotToSend.position.transform.position);
        for (int i = 1; i< inerSpots.Count; i++)
        {
            if(inerSpots[i].enemyQuantity < spotToSend.enemyQuantity)
            {
                spotToSend = inerSpots[i];
                distance = Vector3.Distance(enemy.transform.position, spotToSend.position.transform.position);
            }
            else if(inerSpots[i].enemyQuantity == spotToSend.enemyQuantity)
            {
                float tempDistance = Vector3.Distance(enemy.transform.position, inerSpots[i].position.transform.position);
                if(tempDistance < distance)
                {
                    spotToSend = inerSpots[i];
                }
            }
        }
        spotToSend.enemyQuantity++;
        return spotToSend;
    }

    public void ReceiveDamage(int damage)
    {
        health -= damage;
    }
}