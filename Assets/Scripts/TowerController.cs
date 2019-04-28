using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public enum ShipSize
    {
        small,
        medium,
        big
    }
    public static TowerController instance;
    public SceneLoader sceneLoader;

    public ShipSize currentShipSize = ShipSize.small;
    public ShipSize previousShipSize = ShipSize.small;

    [SerializeField]
    public List<SpotChecker> inerSpots = new List<SpotChecker>();
    public List<SpotChecker> farSpots = new List<SpotChecker>();

    public GameObject smallContainer;
    public GameObject spotSmallParent;
    public GameObject farSmallSpotParent;
    List<SpotChecker> inerSmallSpots = new List<SpotChecker>();
    List<SpotChecker> farSmallSpots = new List<SpotChecker>();

    public GameObject MediContainer;
    public GameObject spotMedParent;
    public GameObject farMedSpotParent;
    List<SpotChecker> inerMedSpots = new List<SpotChecker>();
    List<SpotChecker> farMedSpots = new List<SpotChecker>();

    public GameObject BigContainer;
    public GameObject spotBigParent;
    public GameObject farBigSpotParent;
    List<SpotChecker> inerBigSpots = new List<SpotChecker>();
    List<SpotChecker> farBigSpots = new List<SpotChecker>();

    public int splitIn = 9;

    public HealthController healthController;

    public AudioClip damageSound;
    public AudioSource source; 

    [HideInInspector]
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        target = this.transform;
        healthController = GetComponent<HealthController>();
        for(int i = 0; i < 3; i++)
        {
            int ind = 0;
            switch (i)
            {
                case 0:
                    SpotChecker[] temp = spotSmallParent.GetComponentsInChildren<SpotChecker>();
                    foreach (SpotChecker sp in temp)
                    {
                        sp.spotId = ind;
                        inerSmallSpots.Add(sp);
                        ind++;
                    }
                    ind = 0;
                    SpotChecker[] temp2 = farSmallSpotParent.GetComponentsInChildren<SpotChecker>();
                    foreach (SpotChecker sp in temp2)
                    {
                        sp.spotId = ind;
                        farSmallSpots.Add(sp);
                        ind++;
                    }
                    break;
                case 1:
                    SpotChecker[] temp3 = spotMedParent.GetComponentsInChildren<SpotChecker>();
                    foreach (SpotChecker sp in temp3)
                    {
                        sp.spotId = ind;
                        inerMedSpots.Add(sp);
                        ind++;
                    }
                    ind = 0;
                    SpotChecker[] temp4 = farMedSpotParent.GetComponentsInChildren<SpotChecker>();
                    foreach (SpotChecker sp in temp4)
                    {
                        sp.spotId = ind;
                        farMedSpots.Add(sp);
                        ind++;
                    }
                    break;
                case 2:
                    SpotChecker[] temp5 = spotBigParent.GetComponentsInChildren<SpotChecker>();
                    foreach (SpotChecker sp in temp5)
                    {
                        sp.spotId = ind;
                        inerBigSpots.Add(sp);
                        ind++;
                    }
                    ind = 0;
                    SpotChecker[] temp6 = farBigSpotParent.GetComponentsInChildren<SpotChecker>();
                    foreach (SpotChecker sp in temp6)
                    {
                        sp.spotId = ind;
                        farBigSpots.Add(sp);
                        ind++;
                    }
                    break;
            }
        }
        SwapSpots(inerSmallSpots, farSmallSpots);
    }
    public void SwapSpots(List<SpotChecker> newList, List<SpotChecker> newList2)
    {
        inerSpots.Clear();
        foreach(SpotChecker sp in newList)
        {
            Debug.Log("Im here");
            inerSpots.Add(sp);
        }
        farSpots.Clear();
        foreach (SpotChecker sp in newList2)
        {
            farSpots.Add(sp);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(previousShipSize != currentShipSize)
        {
            previousShipSize = currentShipSize;
            UpdateShipSize(currentShipSize);
        }
    }

    public void UpdateShipSize(ShipSize shipSize)
    {
        currentShipSize = shipSize;

        switch(currentShipSize)
        {
            case ShipSize.small:
                smallContainer.SetActive(true);
                MediContainer.SetActive(false);
                BigContainer.SetActive(false);
                inerSpots = inerSmallSpots;
                farSpots = farSmallSpots;
                splitIn = 9;
                break;
            case ShipSize.medium:
                smallContainer.SetActive(false);
                MediContainer.SetActive(true);
                BigContainer.SetActive(false);
                inerSpots = inerMedSpots;
                farSpots = farMedSpots;
                splitIn = 9;
                break;
            case ShipSize.big:
                smallContainer.SetActive(false);
                MediContainer.SetActive(false);
                BigContainer.SetActive(true);
                inerSpots = inerBigSpots;
                farSpots = farBigSpots;
                splitIn = 14;
                break;
        }
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
        healthController.ReceiveDamage(damage);
    }
}