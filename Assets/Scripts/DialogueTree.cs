using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTree : MonoBehaviour
{
    [Header("UI Objects")]///////////////////////////////////////////////////////////////////////

    //Shader image that the UI enables when a conversation is happening
    //Darkens everything except the player/passenger which should be on a higher sprite layer
    [SerializeField]
    private Image shaderImage;

    [SerializeField]
    private TextMeshProUGUI passengerText;

    [SerializeField]
    private Image passengerBox;

    //The three buttons that the tree uses to display the player's choices
    [SerializeField]
    private Button[] choiceButtons;

    //The dialoge manager that should be placed in scene before hand
    //Has all the dialogue nodes that make this system work
    private DialogueManager dialogueManager;

    [Header("Variables")]///////////////////////////////////////////////////////////////////////

    //bool to prevent edge cases where multiple dialogue trees could be active at once
    private bool isActive;

    //Delay before the choices appear
    [SerializeField]
    private float textDelay;

    [Header("TESTING")]//////////////////////////////////////////////////////////////////////

    [Tooltip("Turn on when testing dialogue, will start the tree after 1 second")]
    [SerializeField]
    private bool isTesting;

    [Tooltip("Enter the index of the node array on the dialogue manager to test, leave at -1 to stay random")]
    [SerializeField]
    private int testingIndex = - 1;


    // Start is called before the first frame update
    void Start()
    {
        //Set the dialogue manager by finding it in the scene based on the script
        dialogueManager = FindObjectOfType<DialogueManager>();

        //Shouldn't start active so set isActive to false
        isActive = false;

        //If we're testing then 
        if (isTesting)
        {
            //Use the coroutine
            //Remove this once or comment it out once we're actually using this in a proper scene
            StartCoroutine(TestingFunction());
        }

    }

    //Function called when the tree should activate
    public bool StartDialogueTree()
    {
        //Make sure that it hasn't already activated to prevent weird behavior
        if(isActive)
        {
            Debug.Log("Dialogue Tree: Already in progress, aborting start.");
            //The dialogue tree didn't start so return false
            return false;
        }

        //Make the shader object visible so that the focus is on the dialogue and choices
        shaderImage.gameObject.SetActive(true);

        //Now that it's active, set isActive to true
        isActive = true;

        // TO DO
        //Tell the game manager that the game state has become the dialogue state
        // TO DO

        //Declare currentNode so that it can be used in the following statements
        DialogueNode currentNode;

        //If we're looking to test a specific dialogue
        if (isTesting && testingIndex >= 0)
        {
            currentNode = dialogueManager.GetDialogueNode(testingIndex);
        }
        else
        {
            //Get a dialogueNode from the dialogueManager
            currentNode = dialogueManager.GetDialogueNode();
        }

        //Enabled the passenger's text box and set the text of it to the corresponding node's
        passengerBox.gameObject.SetActive(true);
        passengerText.SetText(currentNode.passengerText);

        //Wait textDelay seconds before showing the choices
        StartCoroutine(ShowChoiceBoxes(currentNode));


        //The dialogue tree did start so return true
        return true;
    }

    //IEnumerator function to show the dialogue choices based on how many the node has
    private IEnumerator ShowChoiceBoxes(DialogueNode node)
    {
        //Wait textDelay seconds before continuing
        yield return new WaitForSeconds(textDelay);

        //For loop to enable only as many buttons as the node has choices
        for(int i = 0; i < node.choices.Length; i++)
        {
            //Enabled the button
            choiceButtons[i].gameObject.SetActive(true);
            //Setup the button with it's index alongside the node
            choiceButtons[i].GetComponent<DialogueButtonScript>().SetupButton(node, i);
        }
    }

    //Function called by one of the choice buttons
    //Closes the dialogue and communicates with the passenger
    public void ChoiceMade(DialogueNode node, int index)
    {
        //Set the text of the passenger to the response
        passengerText.SetText(node.choices[index].choiceReaction);

        //TO DO
        //Adjust the value of the passenger's mood based on the choice made
        // node.choices[index].choiceValue
        //TO DO

        //Hide the choice buttons after a choice has been made
        CloseChoiceButtons();
    }

    //Function to hide the choice buttons
    public void CloseChoiceButtons()
    {
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].gameObject.SetActive(false);
        }
    }

    //Close the dialogue tree
    //Exiting out and resuming gameplay
    public void CloseDialogueTree()
    {
        //No longer active so set isActive to false
        isActive = false;

        //Hide the non choice gameObjects since choice's should already be hidden
        passengerBox.gameObject.SetActive(false);

        shaderImage.gameObject.SetActive(false);

        // TO DO
        //Tell the game manager that the game state is no longer in the dialogue state
        // TO DO
    }

    //FUNction purely for testing
    //Turns on the dialogue tree 1 second after starting.
    private IEnumerator TestingFunction()
    {
        yield return new WaitForSeconds(1.0f);
        StartDialogueTree();
    }
}
