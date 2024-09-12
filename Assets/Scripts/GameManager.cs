using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool pauseGame = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseGame && Input.GetKey(KeyCode.Escape)) 
        {
            PauseGame();
        }
        else if (Input.GetKey(KeyCode.Escape) )
        {
            ResumeGame();
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
    }
}
