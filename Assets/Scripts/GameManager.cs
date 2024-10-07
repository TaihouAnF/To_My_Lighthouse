using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    ACTIVE,
    PAUSED,
    ASKING,
    CHOOSING,
    FINISHING
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
