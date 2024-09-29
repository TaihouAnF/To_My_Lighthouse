using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ChoiceStruct
{
    //Text that the player will respond with
    [TextArea(2,4)]
    public string choiceText;

    //Text that the passenger will display after the player's selection
    [TextArea(2, 4)]
    public string choiceReaction;

    public bool isPositive;

    [Header("DEPRECATED")]
    //Value of the choice that the player makes to the passenger
    public int choiceValue;
}


[CreateAssetMenu(fileName = "DialogueNode", menuName = "DialogueNode")]
public class DialogueNode : ScriptableObject
{
    [Tooltip("Sentence/Statement that the passenger makes for this dialogue choice")]
    [TextArea(2,4)]
    [SerializeField]
    public string[] passengerText;

    [Tooltip("Dialogue choices made by the player. \n!MAXIMUM OF THREE! \nChoice Text is the response the player makes. \nChoice Value is the addition it makes to the passengers mood; + goes up/ - goes down")]
    [SerializeField]
    public ChoiceStruct[] choices;

}
