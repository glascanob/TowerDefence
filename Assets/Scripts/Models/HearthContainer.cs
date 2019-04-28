using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HearthContainer : MonoBehaviour
{
    public Image sprite;
    public int state = 6;
    public Animator anim;

    public void Start()
    {
        sprite = GetComponent<Image>();
        anim = GetComponent<Animator>();
    }
    public void GetHurt(int damage)
    {
        state -= damage;

        switch (state)
        {
            case 0:
                TowerController.instance.healthController.currentHearts--;
                gameObject.SetActive(false);
                break;
            case 1:
                sprite.sprite = TowerController.instance.healthController.hearthSprites[2];
                break;
            case 2:
                anim.SetTrigger("Pop");
                sprite.sprite = TowerController.instance.healthController.hearthSprites[1];
                break;
            case 3:
                anim.SetTrigger("Hurt");
                break;
            case 4:
                anim.SetTrigger("Hurt");
                break;
            case 5:
                anim.SetTrigger("Hurt");
                break;
        }
    }
}
