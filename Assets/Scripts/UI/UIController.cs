using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameState currentState = GameState.start;

    GameState previousState = GameState.start;
    public Text startText;

    public GameObject StartCOntainer;
    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.start;
    }

    // Update is called once per frame
    void Update()
    {

        if(currentState != previousState)
        {
            previousState = currentState;
            StateManager();
        }
    }

    public void StateManager()
    {
        switch (currentState)
        {
            case GameState.start:
                startText.text = "Wave " + SpawnController.instance.wave;
                break;
            case GameState.inWave:

                break;
            case GameState.shop:
                break;
            case GameState.gameover:
                break;
        }
    }

}

public enum GameState
{
    start,
    inWave,
    shop,
    gameover
}
