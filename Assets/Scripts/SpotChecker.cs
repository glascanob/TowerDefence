using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotChecker : MonoBehaviour
{
    public int spotId;
    public int enemyQuantity;
    public GameObject position;

    // Start is called before the first frame update
    private void Start()
    {
        position = gameObject;
    }

    public void RemoveEnemy()
    {
        enemyQuantity--;
    }
}
