using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PassengerManagerScript : MonoBehaviour
{

    ///Variables//////////////////////
    [SerializeField]
    private GameObject demonMan;

    //Mood of the passenger
    //Goes between 0 - maximumMood
    //Lower is bad, higher is good
    private int currentMood;

    //Pointer to the LightHouseManager in the scene
    private LightHouseManager lighthouseManager;
    
    //Pointer to the game manager in the scene
    private GameManager gameManager;

    //Pointer to the dialogue manager
    private DialogueManager dialogueManager;

    //Private int to track the number of wrong choices made in a row
    //Distance forward is multiplied by this to prevent soft locking situations
    //Never 0, always >=1
    private int numWrong;

    private int numNeg;

    // A counter for positive moves
    private int numCnt;
    [SerializeField] private float duration;

    // Start is called before the first frame update
    void Start()
    {
        //Set the lhm to the lighthousemanager in the scene
        lighthouseManager = FindObjectOfType<LightHouseManager>();

        //Set the game manager to the one in the scene
        gameManager = FindObjectOfType<GameManager>();

        //Set the dialogue manager to the one in the scene
        dialogueManager = FindObjectOfType<DialogueManager>();

        //Set it to 1
        numWrong = 1;

        numNeg = 0;
        numCnt = 0;
    }

    void Update()
    {
        if (numCnt == dialogueManager.GetNumNodes())
        {
            FinishingGame();
        }
    }

    public void AdjustMood(bool positive)
    {
        //Since the story needs all the nodes to run out, the lighthouse should only travel proportionally each step
        float distance = gameManager.GetStartingDistance() / dialogueManager.GetNumNodes();

        //Behaviors that will trigger off the adjusted mood
        //If the reaction as positive
        if(positive)
        {
            //Move distance towards the player
            lighthouseManager.ShouldMove(distance * numWrong, true);
            ++numCnt;

            //Got the correct choice, so reset numWrong back to 1
            numWrong = 1;
        }
        //If it was a negative reaction
        else
        {
            //Move distance away from the player
            lighthouseManager.ShouldMove(distance, false);

            //Revert the index back to the previous so that the player can't lock themselves out
            dialogueManager.RollbackSequentialIndex();

            numNeg++;

            if(numNeg >= 3)
            {
                demonMan.GetComponent<Animator>().SetTrigger("flashTrigger");
                //demonMan.GetComponent<Animation>().Rewind();
                //demonMan.GetComponent<Animation>().Play("DemonFlash");
            }
        }
    }

    public void FinishingGame()
    {
        gameManager.SetGameState(GameState.FINISHING);
        StartCoroutine(StartShowing());
    }

    private IEnumerator StartShowing()
    {
        lighthouseManager.mirror.gameObject.SetActive(true);
        var clr = lighthouseManager.mirror.color;
        float startAlpha = clr.a;
        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            float newA = Mathf.Lerp(startAlpha, 1f, timeElapsed / duration);
            clr.a = newA;
            lighthouseManager.mirror.color = clr;
            yield return null;
        }
        clr.a = 1f;
        lighthouseManager.mirror.color = clr;
        lighthouseManager.lighthouse.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);

        lighthouseManager.flowers.gameObject.SetActive(true);
        clr = lighthouseManager.flowers.color;
        startAlpha = clr.a;
        timeElapsed = 0;
        while (timeElapsed < duration) 
        {
            timeElapsed += Time.deltaTime;
            float newA = Mathf.Lerp(startAlpha, 1f, timeElapsed / duration);
            clr.a = newA;
            lighthouseManager.flowers.color = clr;
            yield return null;
        }
        clr.a = 1f;
        lighthouseManager.flowers.color = clr;


        FindObjectOfType<FadeController>().WhiteoutFade();
    }
}
