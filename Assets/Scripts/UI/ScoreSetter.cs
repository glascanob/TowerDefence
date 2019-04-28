using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSetter : MonoBehaviour
{

    ScoreController sController;
    public Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        sController = FindObjectOfType<ScoreController>();
        sController.scoreText = scoreText;
        //sController.UpdateScore();
    }
}
