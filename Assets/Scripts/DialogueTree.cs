using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTree : MonoBehaviour
{
    [Header("UI Objects")]

    //Shader image that the UI enables when a conversation is happening
    //Darkens everything except the player/passenger which should be on a higher sprite layer
    [SerializeField]
    private Image shaderImage;

    [SerializeField]
    private TextMeshProUGUI passengerText;

    [SerializeField]
    private Image passengerBox;

    [SerializeField]
    private Button[] choiceButtons;

    private DialogueManager dialogueManager;

    [Header("Variables")]

    private bool isActive;

    //Delay before the choices appear
    [SerializeField]
    private float textDelay;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();

        isActive = false;

        StartCoroutine(TestingFunction());

    }

    public bool StartDialogueTree()
    {
        if(isActive)
        {
            Debug.Log("Dialogue Tree: Already in progress, aborting start.");
            return false;
        }

        shaderImage.gameObject.SetActive(true);

        isActive = true;

        // TO DO
        //Tell the game manager that the game state has become the dialogue state
        // TO DO

        DialogueNode currentNode = dialogueManager.GetDialogueNode();

        passengerBox.gameObject.SetActive(true);
        passengerText.SetText(currentNode.passengerText);

        StartCoroutine(ShowChoiceBoxes(currentNode));

        return true;
    }

    private IEnumerator ShowChoiceBoxes(DialogueNode node)
    {
        yield return new WaitForSeconds(textDelay);

        for(int i = 0; i < node.choices.Length; i++)
        {
            choiceButtons[i].gameObject.SetActive(true);

            choiceButtons[i].GetComponent<DialogueButtonScript>().SetupButton(node, i);
        }
    }

    public void ChoiceMade(DialogueNode node, int index)
    {
        passengerText.SetText(node.choices[index].choiceReaction);

        //TO DO
        //Adjust the value of the passenger's mood based on the choice made
        // node.choices[index].choiceValue
        //TO DO

        CloseChoiceButtons();
    }

    public void CloseChoiceButtons()
    {
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].gameObject.SetActive(false);
        }
    }

    public void CloseDialogueTree()
    {
        isActive = false;

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
