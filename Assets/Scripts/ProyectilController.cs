using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilController : MonoBehaviour
{
    public float speed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distToTower = Vector3.Distance(transform.position, TowerController.instance.target.position);
        if (distToTower > 0.1f)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, TowerController.instance.target.position, step);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
