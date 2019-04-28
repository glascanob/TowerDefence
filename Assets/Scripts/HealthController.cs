using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    HearthContainer[] hearthContainers;
    public GameObject hearthParent;

    public Sprite[] hearthSprites;

    public int currentHearts = 3;
    // Start is called before the first frame update
    void Start()
    {
        hearthContainers = hearthParent.GetComponentsInChildren<HearthContainer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveDamage(int damage)
    {
        hearthContainers[currentHearts].GetHurt(damage);
    }
}
