using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCOntroller : MonoBehaviour
{
    public Sprite[] boats;
    public Image boat;
    public GameObject button;
    public GameObject closeShop;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(TowerController.instance.healthController.currentHearts)
        {
            case 1:
                boat.sprite = boats[0];
                break;
            case 2:
                boat.sprite = boats[0];
                break;
            case 3:
                boat.sprite = boats[1];
                break;
            case 4:
                boat.sprite = boats[1];
                break;
            case 5:
                boat.sprite = boats[2];
                break;
        }
    }

    public void UpgradeShip()
    {
        button.SetActive(false);
        TowerController.instance.healthController.UpgradeShip();
        closeShop.SetActive(true);
    }
    public void CloseShop()
    {
        closeShop.SetActive(false);
        UIController.currentState = GameState.start;
    }
}
