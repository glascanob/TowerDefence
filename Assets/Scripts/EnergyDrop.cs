using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrop : MonoBehaviour
{
    public float timer = 2f;
    float curTimer = 0;

    bool key = false;
    bool grabReady = false;
    private void Update()
    {
        if (key)
        {
            curTimer += Time.deltaTime;
            if(curTimer >= timer)
            {
                curTimer = 0;
                key = false;
                grabReady = true;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && grabReady)
        {
            collision.GetComponentInParent<PlayerBehaviour>().energyBoostAmount++;
            Destroy(gameObject);
        } else if (collision.CompareTag("Player"))
        {
            key = true;
        }
    }
}
