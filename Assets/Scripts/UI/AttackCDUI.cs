using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackCDUI : MonoBehaviour
{
    public AttackBehaviour attackBehaviour;

    Image progressBar;
    bool key = false;

    // Start is called before the first frame update
    void Start()
    {
        progressBar = GetComponent<Image>();
        progressBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (key)
        {
            progressBar.fillAmount -= Time.deltaTime / attackBehaviour.cooldownTime;
            if (progressBar.fillAmount <= 0)
            {
                Reset();
                attackBehaviour.inCd = false;
            }
        }
    }

    public void Key()
    {
        key = true;
    }

    public void Reset()
    {
        progressBar.fillAmount = 1;
        key = false;
    }

    public bool InCD()
    {
        return key;
    }
}
