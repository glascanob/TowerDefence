using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public static SpawnController instance;

    public List<GameObject> spawningPoints;
    public List<GameObject> listOfEnemies;

    public int waveIntensity = 10;
    public float spawningSpeed = 0.5f;

    int wave = 0;
    public bool inWave = false;
    bool spawning = false;

    public Dictionary<int,GameObject> aliveEnemies;
    int enemyIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        aliveEnemies = new Dictionary<int, GameObject>;
    }

    // Update is called once per frame
    void Update()
    {
        if (inWave)
        {
            if (aliveEnemies.Count == 0)
            {
                if (!spawning)
                {
                    spawning = true;
                    SpawnEnemies();
                    //switch (wave)
                    //{
                    //    case 0:
                    //        for (int i = 0; i < wave + 1; i++)
                    //        {
                    //            SpawnEnemies();
                    //        }
                    //        break;
                    //    case 1:
                    //        break;
                    //    case 2:
                    //        break;
                    //    case 3:
                    //        break;
                    //    case 4:
                    //        break;
                    //    case 5:
                    //        break;
                    //}
                }
            }
        }
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < wave + 1; i++)
        {
            StartCoroutine(SpawnEnemiesCo(Random.Range(0, spawningPoints.Count)));
        }
        spawning = false;
    }

    IEnumerator SpawnEnemiesCo(int spawningPoint)
    {
        int numberOfenemies = Random.Range(wave + waveIntensity / 2, wave * waveIntensity);

        for(int i = 0; i < numberOfenemies; i++)
        {
            int typeofEnemy = Random.Range(0, wave < listOfEnemies.Count ? wave : listOfEnemies.Count);
            GameObject newEnemy = Instantiate(listOfEnemies[typeofEnemy], spawningPoints[spawningPoint].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyBehavior>().index = enemyIndex++;
            aliveEnemies.Add(enemyIndex, newEnemy);
            yield return new WaitForSeconds(spawningSpeed);
        }
    }

    public void KillEnemy(int index)
    {
        inWave = aliveEnemies.Count - 1 == 0 ? false : true;
        aliveEnemies.Remove(index);
    }
}
