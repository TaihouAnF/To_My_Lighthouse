using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerManagerScript : MonoBehaviour
{

    ///Variables//////////////////////
    [Tooltip("Starting mood value of the passenger when the player starts the game, should be lower rather than higher")]
    [SerializeField]
    private int startingMood;

    [Tooltip("Maximum mood value of the passenger")]
    [SerializeField]
    private int maximumMood;

    //Mood of the passenger
    //Goes between 0 - maximumMood
    //Lower is bad, higher is good
    private int currentMood;

    // Start is called before the first frame update
    void Start()
    {
        //Set the currentMood to the startingMood
        currentMood = startingMood;
    } 

    public void AdjustMood(int value)
    {
        //Add the value to this currentMood
        currentMood += value;
        
        //Clamp the value so it can't go over the maximum set OR below 0
        Mathf.Clamp(currentMood, 0, maximumMood);

        //TO DO
        //Behaviors that will trigger off the adjusted mood
        //Things like the lighthouse getting closer and visual fx
        //TO DO

        //Debug statement for evaluating the passenger's mood
        Debug.Log("Passenger Manager: CurrentMood = " + currentMood);
    }
}
