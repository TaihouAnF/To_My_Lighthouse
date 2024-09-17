using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    //Points to the Dialogue Tree that should exist in the game scene
    //[SerializeField]
    private DialogueTree dialogueTree;

    //Holds all the dialogueNodes that the player can encounter with the passenger
    //When sent to the dialogue tree it should not repeat.
    [SerializeField]
    private DialogueNode[] dialogueNodes;

    //array to hold all the indicies of the used dialogue nodes to prevent repeats
    //False means not used
    //True means used
    private bool[] usedIndices;

    private void Start()
    {

        dialogueTree = FindObjectOfType<DialogueTree>();

        //Set the array to be the size of the dialogueNode array
        usedIndices = new bool[dialogueNodes.Length];

        //Set all the values of the array to false at the start
        ResetUsedIndices();

    }

    //Function called by the dialogue tree when it opens to get a dialogue to load with the player
    //Also adds the index to the used list so that repeats don't happen until they have all cycled through
    public DialogueNode GetDialogueNode()
    {
        //Get a random int between 0 and the amount of dialogueNode we have
        int randomIndex;

        //Do while loop that will repeat until the randomIndex it found has not been used yet
        do
        {
            randomIndex = Random.Range(0, dialogueNodes.Length);
        } while (usedIndices[randomIndex]);

        //Make sure one final time that the dialogue node actually exists at the array
        if (dialogueNodes[randomIndex] != null)
        {

            //Check the indices
            CheckUsedIndicies();

            //Return the dialogue node to the dialogue tree
            return dialogueNodes[randomIndex];
        }
        //Otherwise
        else
        {
            //Return null
            return null;
        }
    }

    //Same as the above function but takes a parameter for a specific index
    public DialogueNode GetDialogueNode(int index)
    {
        //Make sure one final time that the dialogue node actually exists at the array
        if (dialogueNodes[index] != null)
        {

            //Check the indices
            CheckUsedIndicies();

            //Return the dialogue node to the dialogue tree
            return dialogueNodes[index];
        }
        //Otherwise
        else
        {
            //Return null
            return null;
        }
    }

    //Function to reset all the values of the array to false
    private void ResetUsedIndices()
    {
        //Basic For Loop to go through and set all values of the array to false
        for (int i = 0; i < dialogueNodes.Length; i++)
        {
            usedIndices[i] = false;
        }
    }

    //Function to check all the used indices.
    //If all indices have been used then reset the array
    //Otherwise do nothing
    private bool CheckUsedIndicies()
    {
        //Always start with 0 true
        int trueCount = 0;

        //Basic for loop to iterate through the index array and sum up the number of trues.
        for(int i = 0; i <usedIndices.Length; i++)
        {
            if (usedIndices[i])
            {
                trueCount++;
            }
        }

        //If all the indices were true
        if (trueCount == usedIndices.Length)
        {
            //Then reset the array
            ResetUsedIndices();
            Debug.Log("Dialogue Manager: All indices used, resetting the array");
            //All indicies were used so return true
            return true;
        }
        else
        {
            //If not all indices were true then return false
            Debug.Log("DialogueManager: Not all nodes have been used");
            return false;
        }
    }
}
