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
    public bool canBuy = false;

    public PlayerBehaviour player;

    public AttackBehaviour[] attackBehaviours;

    int previousHearths = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (previousHearths != TowerController.instance.healthController.currentHearts)
        {
            switch (TowerController.instance.healthController.currentHearts)
            {
                case 1:
                    boat.sprite = boats[0];
                    TowerController.instance.UpdateShipSize(TowerController.ShipSize.small);
                    break;
                case 2:
                    boat.sprite = boats[0];
                    TowerController.instance.UpdateShipSize(TowerController.ShipSize.small);
                    break;
                case 3:
                    boat.sprite = boats[1];
                    TowerController.instance.UpdateShipSize(TowerController.ShipSize.medium);
                    break;
                case 4:
                    boat.sprite = boats[1];
                    TowerController.instance.UpdateShipSize(TowerController.ShipSize.medium);
                    break;
                case 5:
                    boat.sprite = boats[2];
                    TowerController.instance.UpdateShipSize(TowerController.ShipSize.big);
                    break;
            }
            previousHearths = TowerController.instance.healthController.currentHearts;
        }
    }

    public void UpgradeShip()
    {
        if (!canBuy)
            return;
        canBuy = true;
        TowerController.instance.healthController.UpgradeShip();
        closeShop.SetActive(true);
    }
    public void UpgradeLightAttack()
    {
        if (!canBuy)
            return;
        canBuy = true;
        attackBehaviours[0].damage++;
        closeShop.SetActive(true);
    }
    public void UpgradeHeavyAttack()
    {
        if (!canBuy)
            return;
        canBuy = true;
        attackBehaviours[1].damage++;
        closeShop.SetActive(true);
    }
    public void UpgradeAirAttack()
    {
        if (!canBuy)
            return;
        canBuy = true;
        attackBehaviours[2].damage++;
        closeShop.SetActive(true);
    }
    public void UpgradeRangedAttack()
    {
        if (!canBuy)
            return;
        canBuy = true;
        attackBehaviours[3].damage++;
        closeShop.SetActive(true);
    }
    public void UpgradeSpawnAttack()
    {
        if (!canBuy)
            return;
        canBuy = true;
        attackBehaviours[4].damage++;
        closeShop.SetActive(true);
    }
    public void UpgradeSpeed()
    {
        if (!canBuy)
            return;
        canBuy = true;
        player.moveSpeed += 0.5f;
        closeShop.SetActive(true);
    }
    public void CloseShop()
    {
        closeShop.SetActive(false);
        UIController.currentState = GameState.start;
    }
}
