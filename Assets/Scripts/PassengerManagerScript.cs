using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerManagerScript : MonoBehaviour
{

    ///Variables//////////////////////
    [SerializeField]
    private GameObject demonMan;

    //Mood of the passenger
    //Goes between 0 - maximumMood
    //Lower is bad, higher is good
    private int currentMood;

    //Pointer to the LightHouseManager in the scene
    private LightHouseManager lighthouseManager;
    
    //Pointer to the game manager in the scene
    private GameManager gameManager;

    //Pointer to the dialogue manager
    private DialogueManager dialogueManager;

    //Private int to track the number of wrong choices made in a row
    //Distance forward is multiplied by this to prevent soft locking situations
    //Never 0, always >=1
    private int numWrong;

    private int numNeg;

    // Start is called before the first frame update
    void Start()
    {
        //Set the lhm to the lighthousemanager in the scene
        lighthouseManager = FindObjectOfType<LightHouseManager>();

        //Set the game manager to the one in the scene
        gameManager = FindObjectOfType<GameManager>();

        //Set the dialogue manager to the one in the scene
        dialogueManager = FindObjectOfType<DialogueManager>();

        //Set it to 1
        numWrong = 1;

        numNeg = 0;
    } 

    public void AdjustMood(bool positive)
    {
        //Since the story needs all the nodes to run out, the lighthouse should only travel proportionally each step
        float distance = gameManager.GetStartingDistance() / dialogueManager.GetNumNodes();

        //Behaviors that will trigger off the adjusted mood
        //If the reaction as positive
        if(positive)
        {
            //Move distance towards the player
            lighthouseManager.ShouldMove(distance * numWrong, true);

            //Got the correct choice, so reset numWrong back to 1
            numWrong = 1;
        }
        //If it was a negative reaction
        else
        {
            //Move distance away from the player
            lighthouseManager.ShouldMove(distance, false);

            //Revert the index back to the previous so that the player can't lock themselves out
            dialogueManager.RollbackSequentialIndex();

            numNeg++;

            if(numNeg >= 3)
            {
                demonMan.SetActive(true);
            }
        }
    }
}
