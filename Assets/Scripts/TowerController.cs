using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public static TowerController instance;

    [HideInInspector]
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        target = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
