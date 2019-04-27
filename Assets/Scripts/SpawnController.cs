using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public static SpawnController instance;

    public Transform[] spawningPoints;
    public GameObject spawningPointsParent;
    public List<GameObject> listOfEnemies;

    public int waveIntensity = 10;
    public float spawningSpeed = 0.5f;

    public int wave = 0;
    public bool inWave = false;
    bool spawning = false;

    public Dictionary<int,GameObject> aliveEnemies;
    int enemyIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        aliveEnemies = new Dictionary<int, GameObject>();
        spawningPoints = spawningPointsParent.GetComponentsInChildren<Transform>();
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
            StartCoroutine(SpawnEnemiesCo(Random.Range(1, spawningPoints.Length)));
        }
        spawning = false;
    }

    IEnumerator SpawnEnemiesCo(int spawningPoint)
    {
        int numberOfenemies = Random.Range(wave + waveIntensity / 2, wave * waveIntensity / 2);

        for(int i = 0; i < numberOfenemies; i++)
        {
            int typeofEnemy = Random.Range(0, wave < listOfEnemies.Count ? wave : listOfEnemies.Count);
            GameObject newEnemy = Instantiate(listOfEnemies[typeofEnemy], spawningPoints[spawningPoint].position, Quaternion.identity);
            newEnemy.GetComponentInChildren<EnemyBehavior>().index = enemyIndex;
            aliveEnemies.Add(enemyIndex, newEnemy);
            enemyIndex++;
            yield return new WaitForSeconds(spawningSpeed);
        }
    }

    public void KillEnemy(int index)
    {
        if (aliveEnemies.Count - 1 <= 0)
        {
            inWave = false;
        }
        GameObject enemyToKill;
        aliveEnemies.TryGetValue(index, out enemyToKill);
        aliveEnemies.Remove(index);
        EnemyBehavior enemy = enemyToKill.GetComponentInChildren<EnemyBehavior>();
        if(enemy.target != null)
        {
            enemy.target.RemoveEnemy();
        }
        Destroy(enemyToKill);
    }
}
