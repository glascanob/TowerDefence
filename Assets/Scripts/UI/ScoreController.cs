using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;
    public Text scoreText;

    public int score;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

            DontDestroyOnLoad(gameObject);

        //UpdateScore();
    }

    public void UpdateScore(int wave)
    {
        //scoreText.text = spawnController.wave.ToString();
        score = wave;
    }
}
