using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
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

    //The actual text that the passenger will state
    //UI element
    [SerializeField]
    private TextMeshProUGUI passengerText;

    //The dialoge manager that should be placed in scene before hand
    //Has all the dialogue nodes that make this system work
    private DialogueManager dialogueManager;

    [SerializeField]
    private TextMeshPro lighthouseText;

    [SerializeField]
    private TextMeshPro edgeText;

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

    [Tooltip("Value multiplied by the total length of the sun text box")]
    [SerializeField]
    private float characterSize;

    [SerializeField]
    private Image sunBoxRenderer;

    [Header("TESTING")]//////////////////////////////////////////////////////////////////////

    [Tooltip("Turn on when testing dialogue, will start the tree after 1 second")]
    [SerializeField]
    private bool isTesting;

    [Tooltip("Enter the index of the node array on the dialogue manager to test, leave at -1 to stay random")]
    [SerializeField]
    private int testingIndex = -1;

    //Pointer to the passenger manager script that the dialogue choices will interact with
    private PassengerManagerScript passengerManager;

    //Enum that tracks the current state of the Dialogue Tree
    private DialogueTreeState currentState;

    //Variable that holds the node currently being judged by the player
    private DialogueNode currentActiveNode;

    private bool isOver;


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

        isOver = false;

    }

    void Update()
    {
        //If we're waiting for an input and the input is pressed
        if (currentState == DialogueTreeState.WAITING && Input.GetKeyDown(dialogueKey))
        {
            CloseDialogueTree();
        }
    }

    //Function called when the tree should activate
    public bool StartDialogueTree()
    {
        //Make sure that it hasn't already activated to prevent weird behavior
        if (isActive || isOver)
        {
            Debug.Log("Dialogue Tree: Already in progress, aborting start.");
            //The dialogue tree didn't start so return false
            return false;
        }

        currentState = DialogueTreeState.ACTIVE;

        //Now that it's active, set isActive to true
        isActive = true;

        //Declare currentNode so that it can be used in the following statements
        DialogueNode currentNode;

        //If we're looking to test a specific dialogue
        if (isTesting && testingIndex >= 0)
        {
            currentNode = dialogueManager.GetDialogueNode(testingIndex);
        }
        else
        {
            //Switched to sequential story telling rather than random
            //Using the new function instead
            currentNode = dialogueManager.GetSequentialDialogueNode();
            if (currentNode == null)
            {
                return false;
            }

            currentActiveNode = currentNode;
        }

        StopCoroutine(TypewriterTextSun(passengerText));
        StartCoroutine(TypewriterTextSun(passengerText));

        //The dialogue tree did start so return true
        return true;
    }

    //Function called by one of the choice buttons
    //Closes the dialogue and communicates with the passenger
    public void ChoiceMade(DialogueNode node, int index)
    {
        //Adjust the value of the passenger's mood based on the choice made
        passengerManager.AdjustMood(node.choices[index].isPositive);

        if(currentActiveNode.isFinal)
        {
            isOver = true;
        }
    }

    //Different version that uses the current saved node
    public void ChoiceMade(int index)
    {
        passengerManager.AdjustMood(currentActiveNode.choices[index].isPositive);

        passengerText.text = string.Empty;
        lighthouseText.text = string.Empty;
        edgeText.text = string.Empty;

        //Function to remove the pop up text;
    }

    //Close the dialogue tree
    //Exiting out and resuming gameplay
    public void CloseDialogueTree()
    {
        //No longer active so set isActive to false
        isActive = false;

        currentState = DialogueTreeState.INACTIVE;

        //
        //Tell the game manager that the game state is no longer in the dialogue state
        //

        lighthouseText.text = string.Empty;
        edgeText.text = string.Empty;

        dialogueManager.FinishDialogue();

    }


    private IEnumerator TypewriterTextSun(TextMeshProUGUI tmp)
    {

        for (int i = 0; i < currentActiveNode.passengerText.Length; i++)
        {

            Vector3 boxVector = new Vector3(characterSize * currentActiveNode.passengerText[i].Length, characterSize * 2, 1);
            sunBoxRenderer.transform.localScale = boxVector;

            tmp.text = string.Empty;

            foreach (char c in currentActiveNode.passengerText[i])
            {
                tmp.text = tmp.text + c;
                if (c == ' ') { yield return new WaitForSeconds(0); }
                else { yield return new WaitForSeconds(characterDelay); }
            }

            yield return new WaitForSeconds(textDelay);
        }

        yield return new WaitForSeconds(textDelay);

        FindObjectOfType<GameManager>().SetGameState(GameState.ASKING);

        StartCoroutine(TypewriterTextChoice(lighthouseText, 0));
        StartCoroutine(TypewriterTextChoice(edgeText, 1));
    }

    private IEnumerator TypewriterTextChoice(TextMeshPro tmp, int choiceIndex)
    {
        tmp.text = string.Empty;

        foreach (char c in currentActiveNode.choices[choiceIndex].choiceText)
        {

            tmp.text = tmp.text + c;

            if (c == ' ')
            {
                yield return new WaitForSeconds(0);
            }
            else
            {
                yield return new WaitForSeconds(characterDelay);
            }
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
