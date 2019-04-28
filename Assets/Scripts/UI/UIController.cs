using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static GameState currentState = GameState.start;

    GameState previousState = GameState.start;
    public Text startText;

    public GameObject startContainer;
    public GameObject shopContainer;
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
                startContainer.SetActive(true);
                shopContainer.SetActive(false);
                break;
            case GameState.inWave:
                startContainer.SetActive(false);
                shopContainer.SetActive(false);
                break;
            case GameState.shop:
                startContainer.SetActive(false);
                shopContainer.SetActive(true);
                break;
            case GameState.gameover:
                SceneLoader.instance.LoadGameOverScene();
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
