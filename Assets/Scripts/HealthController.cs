using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    public HearthContainer[] hearthContainers;
    public GameObject hearthParent;

    public Sprite[] hearthSprites;

    public int currentHearts = 3;
    // Start is called before the first frame update
    void Start()
    {
        //hearthContainers = hearthParent.GetComponentsInChildren<HearthContainer>();
        for(int i = 0; i< currentHearts; i++)
        {
            hearthContainers[i].GetHurt(-6);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHearts <= 0)
        {
            UIController.currentState = GameState.gameover;
        }
    }

    public void ReceiveDamage(int damage)
    {
        hearthContainers[currentHearts-1].GetHurt(damage);
    }
    public void UpgradeShip()
    {
        hearthContainers[currentHearts - 1].GetHurt(hearthContainers[currentHearts - 1].state - 6);
        currentHearts++;
        hearthContainers[currentHearts - 1].GetHurt(-6);
    }
}
