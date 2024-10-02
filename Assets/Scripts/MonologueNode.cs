using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonologueNode", menuName = "MonologueNode")]
public class MonologueNode : ScriptableObject
{
    [Tooltip("Vector2 that determines where the dialogue will appear")]
    [SerializeField]
    public Vector2 locationVector;

    [Tooltip("Sets size")]
    [SerializeField]
    public Vector2 sizeVector;

    [Tooltip("Sentence/Statement that will be displayed in the inner thoughts")]
    [TextArea(2, 4)]
    [SerializeField]
    public string[] monologueText;

    [Tooltip("Check if this node has one that follows it")]
    [SerializeField]
    public bool notLastNode;

    [Tooltip("Delay for next node in sequence, not used if last node")]
    [SerializeField]
    public float nextDelay;
}
