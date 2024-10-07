using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonologueBoxScript : MonoBehaviour
{
    [SerializeField]
    private Image backgroundImage;

    [SerializeField]
    private TextMeshProUGUI tmp;

    private Vector2 backgroundSize;

    private MonologueNode thisNode;

    [SerializeField]
    private float characterDelay;

    [SerializeField]
    private float lifeSpan;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setup(MonologueNode node)
    {
        SetBoxSize(node.sizeVector);

        thisNode = node;

        StartCoroutine(RevealText(thisNode, false,0));
    }

    private void SetBoxSize(Vector2 size)
    {
        backgroundSize = size;

        backgroundImage.rectTransform.sizeDelta = backgroundSize;

        tmp.rectTransform.sizeDelta = backgroundSize;
    }

    public void SetLifeSpan(float duration)
    {
        lifeSpan = duration;
    }

    //Coroutine to display the text in a type writer like fashion, one character by one
    private IEnumerator RevealText(MonologueNode node, bool isReaction, int index)
    {
        //Empty the textbox of anything that was previously in it
        tmp.text = string.Empty;

        //Go through each character in the text
        foreach (char c in node.monologueText[0])
        {

            //Add each character to the text box
            tmp.text = tmp.text + c;

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

        SetLifeSpan(lifeSpan);

        StartCoroutine(MonologueFinished());

    }

    private IEnumerator MonologueFinished()
    {
        yield return new WaitForSeconds(lifeSpan);

        if (thisNode.notLastNode)
        {
            FindObjectOfType<DialogueManager>().CalledByBox(thisNode.nextDelay);
        }

        Destroy(gameObject);
    }

}

