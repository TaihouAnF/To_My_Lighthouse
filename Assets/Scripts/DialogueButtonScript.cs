using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueButtonScript : MonoBehaviour
{
    [SerializeField]
    private DialogueTree dialogueTree;

    //pointer to the passenger's script

    //DialogueNode associated with this button
    private DialogueNode thisNode;

    //the index that this button has
    private int thisIndex;

    //The text of this button
    [SerializeField]
    private TextMeshProUGUI tmp;

    //Set by the dialogue tree when populating the buttons
    public void SetupButton(DialogueNode node,int index)
    {
        //set the value of the choice
        thisNode = node;
        thisIndex = index;
        GetComponentInChildren<TextMeshProUGUI>().SetText(thisNode.choices[index].choiceText);
    }
    
    //Called when the button is clicked by the player
    public void OnClick()
    {
        //Tell the dialogue tree to perform the response associated with the node
        dialogueTree.ChoiceMade(thisNode, thisIndex); 
    }
}
