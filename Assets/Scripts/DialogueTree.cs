using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum DialogueTreeState
{
    INACTIVE,
    ACTIVE,
    WAITING
}

public class DialogueTree : MonoBehaviour
{
    [Header("UI Objects")]///////////////////////////////////////////////////////////////////////

    //Shader image that the UI enables when a conversation is happening
    //Darkens everything except the player/passenger which should be on a higher sprite layer
    [SerializeField]
    private Image shaderImage;

    //The actual text that the passenger will state
    //UI element
    [SerializeField]
    private TextMeshProUGUI passengerText;

    //Textbox that the passenger text will be nested in
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
    [Tooltip("THe amount of time in seconds after the text has finished displaying that the choices will appear")]
    [SerializeField]
    private float textDelay;

    [Tooltip("The amount of time in seconds between each character of the passenger's text appearing")]
    [SerializeField]
    private float characterDelay;

    [Tooltip("The Key that is pressed to advance the dialogue to the next scene")]
    [SerializeField]
    private KeyCode dialogueKey;

    [Header("TESTING")]//////////////////////////////////////////////////////////////////////

    [Tooltip("Turn on when testing dialogue, will start the tree after 1 second")]
    [SerializeField]
    private bool isTesting;

    [Tooltip("Enter the index of the node array on the dialogue manager to test, leave at -1 to stay random")]
    [SerializeField]
    private int testingIndex = - 1;

    //Pointer to the passenger manager script that the dialogue choices will interact with
    private PassengerManagerScript passengerManager;

    //Enum that tracks the current state of the Dialogue Tree
    private DialogueTreeState currentState;

    // Start is called before the first frame update
    void Start()
    {
        //Set the dialogue manager by finding it in the scene based on the script
        dialogueManager = FindObjectOfType<DialogueManager>();

        //Set the passenger manager by finding it in the scene based on the script
        passengerManager = FindObjectOfType<PassengerManagerScript>();

        //Shouldn't start active so set isActive to false
        isActive = false;

        currentState = DialogueTreeState.INACTIVE;

        //If we're testing then 
        if (isTesting)
        {
            //Use the coroutine
            //Remove this once or comment it out once we're actually using this in a proper scene
            StartCoroutine(TestingFunction());
        }

    }

    void Update()
    {
        //If we're waiting for an input and the input is pressed
        if(currentState == DialogueTreeState.WAITING && Input.GetKeyDown(dialogueKey))
        {
            CloseDialogueTree();
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

        currentState = DialogueTreeState.ACTIVE;

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
            //currentNode = dialogueManager.GetDialogueNode();

            //Switched to sequential story telling rather than random
            //Using the new function instead
            currentNode = dialogueManager.GetSequentialDialogueNode();
            if(currentNode == null)
            {
                return false;
            }
        }

        //Enabled the passenger's text box and set the text of it to the corresponding node's
        passengerBox.gameObject.SetActive(true);

        StartCoroutine(RevealText(currentNode, false, 0));
        //passengerText.SetText(currentNode.passengerText);

        //Wait textDelay seconds before showing the choices
        //StartCoroutine(ShowChoiceBoxes(currentNode));

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
        //passengerText.SetText(node.choices[index].choiceReaction);

        //Adjust the value of the passenger's mood based on the choice made
        passengerManager.AdjustMood(node.choices[index].choiceValue);

        //Hide the choice buttons after a choice has been made
        CloseChoiceButtons();

        StartCoroutine(RevealText(node, true, index));
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

        currentState = DialogueTreeState.INACTIVE;

        //Hide the non choice gameObjects since choice's should already be hidden
        passengerBox.gameObject.SetActive(false);
        shaderImage.gameObject.SetActive(false);


        //Tell the game manager that the game state is no longer in the dialogue state
        dialogueManager.FinishDialogue();
    }

    //Coroutine to display the text in a type writer like fashion, one character by one
    private IEnumerator RevealText(DialogueNode node, bool isReaction, int index)
    {
        //Empty the textbox of anything that was previously in it
        passengerText.text = string.Empty;

        //If this is the reaction text then we need a different part of DialogueNode
        if (isReaction)
        {
            //Go through each character in the text
            foreach (char c in node.choices[index].choiceReaction)
            {
                //Add each character to the text box
                passengerText.text = passengerText.text + c;

                if (c == ' ')
                {
                    //Skip the delay if it's a space
                    yield return new WaitForSeconds(0);
                }
                else
                {
                    //Wait characterDelay seconds before adding another character
                    yield return new WaitForSeconds(characterDelay);
                }
            }

            currentState = DialogueTreeState.WAITING;
        }
        //If this is not the reaction text then it's a lot simpler
        else
        {
            //Go throuhg each character in the text
            foreach (char c in node.passengerText[0])
            {

                //Add each character to the text box
                passengerText.text = passengerText.text + c;

                if (c == ' ')
                {   
                    //Skip the delay if it's a space
                    yield return new WaitForSeconds(0);
                }
                else
                {
                    //Wait characterDelay seconds before adding another character
                    yield return new WaitForSeconds(characterDelay);
                }
            }

            //Since this is the first display
            //Show the player their choices after a brief delay
            StartCoroutine(ShowChoiceBoxes(node));
        }

    }

    //FUNction purely for testing
    //Turns on the dialogue tree 1 second after starting.
    private IEnumerator TestingFunction()
    {
        yield return new WaitForSeconds(1.0f);
        StartDialogueTree();
    }
}
