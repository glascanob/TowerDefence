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
    public float spawningSpeed = 8f;

    public int wave = 0;
    public bool inWave = false;

    public int totalEnemiesInWave = 0;

    int spawningActivePoints = 0;
    bool spawning = false;

    public Dictionary<int,GameObject> aliveEnemies;
    int enemyIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        inWave = false;
        instance = this;
        aliveEnemies = new Dictionary<int, GameObject>();
        spawningPoints = spawningPointsParent.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inWave)
        {
            if (totalEnemiesInWave == 0)
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
        if(spawningActivePoints == 0)
        {
            spawning = false;
        }
    }

    public void StartWave()
    {
        inWave = true;
        UIController.currentState = GameState.inWave;
    }

    public void SpawnEnemies()
    {
        spawningActivePoints = wave;

        for (int i = 0; i < wave + 1; i++)
        {
            int numberOfenemies = Random.Range(wave + waveIntensity / 2, wave * waveIntensity / 3);
            totalEnemiesInWave += numberOfenemies;
            StartCoroutine(SpawnEnemiesCo(Random.Range(1, spawningPoints.Length),numberOfenemies));
        }
        //spawning = false;
    }

    IEnumerator SpawnEnemiesCo(int spawningPoint, int numberOfenemies)
    {
        yield return new WaitForSeconds(Random.Range(Mathf.Clamp(spawningSpeed/4 - wave, 0.8f, 100f), Mathf.Clamp(spawningSpeed/2 - wave, 0.8f, 100f)));
        //numberOfenemies = 1;
        for(int i = 0; i < numberOfenemies; i++)
        {
            yield return new WaitForSeconds(Random.Range(Mathf.Clamp(spawningSpeed - wave, 0.8f, 100f), Mathf.Clamp(spawningSpeed * 2 - wave, 0.8f, 100f)));
            int typeofEnemy = Random.Range(0, wave < listOfEnemies.Count ? wave : listOfEnemies.Count);
            //typeofEnemy = 2;
            //spawningPoint = 6;
            GameObject newEnemy = Instantiate(listOfEnemies[typeofEnemy], spawningPoints[spawningPoint].position, Quaternion.identity);
            newEnemy.GetComponentInChildren<SpriteRenderer>().flipX = spawningPoint % 2 == 0 ? true : false;
            newEnemy.GetComponentInChildren<EnemyBehavior>().index = enemyIndex;
            aliveEnemies.Add(enemyIndex, newEnemy);
            enemyIndex++;
        }
        spawningActivePoints--;
    }

    public void KillEnemy(int index)
    {
        if (totalEnemiesInWave - 1 <= 0 && !spawning)
        {
            inWave = false;
            UIController.currentState = GameState.shop;
            wave++;
            ScoreController.instance.score = wave;
            CameraController.instance.ToggleCamera(wave);
        }
        GameObject enemyToKill;
        aliveEnemies.TryGetValue(index, out enemyToKill);
        aliveEnemies.Remove(index);
        EnemyBehavior enemy = enemyToKill.GetComponentInChildren<EnemyBehavior>();
        if(enemy.target != null)
        {
            enemy.target.RemoveEnemy();
        }
        Destroy(enemyToKill, 0.1f);
        totalEnemiesInWave--;
    }
}
