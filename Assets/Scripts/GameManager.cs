using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool pauseGame = false;
    public Button menuButton;
    public Button resumeButton;

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
}
