using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    ACTIVE,
    PAUSED
}


public class GameManager : MonoBehaviour
{
    public bool pauseGame = false;
    public Button menuButton;
    public Button resumeButton;

    [Tooltip("The starting distance between the lighthouse and the player")]
    [SerializeField]
    private int startingDistance;

    private GameState currentGameState;

    private void Start()
    {

        currentGameState = GameState.ACTIVE;

        //Brief formula to set the lighthouse startingDistance away from the player
        //Player can't truly "rotate" so just modify the Z value
        Vector3 lighthouseStartingPos = FindObjectOfType<LightHouseManager>().transform.position;
        lighthouseStartingPos.z = startingDistance;
        FindObjectOfType<LightHouseManager>().transform.position = lighthouseStartingPos;
        FindObjectOfType<WrappingHorizonScript>().UpdateDistance(startingDistance);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseGame && Input.GetKeyDown(KeyCode.Escape)) 
        {
            PauseGameByClick();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGameByPress();
        }
    }

    public void PauseGame() 
    {
        // OnGamePause?.Invoke();
        // TODO: Panel to appear
        pauseGame = true;
        Time.timeScale = 0;
        
    }

    public void ResumeGame() 
    {
        // TODO: Panel disappear
        pauseGame = false;
        Time.timeScale = 1;
        menuButton.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(false);
    }

    public void PauseGameByClick() 
    {
        PauseGame();
        menuButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(true);
    }

    public void ResumeGameByPress() 
    {
        ResumeGame();
        menuButton.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(false);
    }

    public int GetStartingDistance()
    {
        return startingDistance;
    }

    public GameState GetGameState()
    {
        return currentGameState;
    }

    public void SetGameState(GameState gameState)
    {
        currentGameState = gameState;
    }
}
