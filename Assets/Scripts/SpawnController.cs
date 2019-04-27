using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public List<GameObject> spawningPoints;

    public int waveIntensity = 10;
    public float spawningSpeed = 0.5f;

    int wave = 0;
    bool inWave = true;
    bool spawning = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inWave)
        {
            switch (wave)
            {
                case 0:
                    for(int i = 0; i< wave+1; i++)
                    {
                         SpawnEnemies(Random.Range(0, spawningPoints.Count));
                    }
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
        }
    }

    public void SpawnEnemies(int spawningPoint)
    {
        StartCoroutine(SpawnEnemiesCo(spawningPoint));
    }

    IEnumerator SpawnEnemiesCo(spawningPoint)
    {
        int numberOfenemies = Random.Range(wave + waveIntensity / 2, wave * waveIntensity);

        for(int i = 0; i < numberOfenemies; i++)
        {

        }
    }
}
