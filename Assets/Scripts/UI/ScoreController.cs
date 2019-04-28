using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;
    public Text scoreText;
    public int curScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        int scoreControllerCount = FindObjectsOfType<ScoreController>().Length;
        if (scoreControllerCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        UpdateScore();
    }

    public void AddScore(int score)
    {
        curScore += score;
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = curScore.ToString();
    }
}
