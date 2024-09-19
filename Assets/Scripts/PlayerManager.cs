using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Attributes")]
    public float playerRotateSpeed;
    private float horizontalInput;
    private Rigidbody rb;

    [Tooltip("Abs of the angle that the player must be less than on the Y axis to count as charging since they'll be facing the lighthouse")]
    [SerializeField]
    private float angleCheck;
    // Start is called before the first frame update

    [Tooltip("The value that determines in seconds how long the player has to look at the lighthouse before being prompted to answer a dialogue")]
    [SerializeField]
    private float dialogueChargeTime;
    private float currentChargeTime;

    private DialogueManager dialogueManager;
    private GameManager gameManager;
    private LightHouseManager lightHouseManager;

    void Start()
    {

        currentChargeTime = 0;

        rb = GetComponent<Rigidbody>();

        dialogueManager = FindObjectOfType<DialogueManager>();
        gameManager = FindObjectOfType<GameManager>();
        lightHouseManager = FindObjectOfType<LightHouseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Only update the rotation if the game state is active
        if (gameManager.GetGameState() == GameState.ACTIVE)
        {
            UpdatePlayerRotation();

            CheckCharge();
        }

    }

    /// <summary>
    ///  Controlling rotation of the player.
    /// </summary>
    private void UpdatePlayerRotation() 
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, playerRotateSpeed * horizontalInput * Time.deltaTime);
    }

    private void CheckCharge()
    {
        //Only count the charge when the game state is ACTIVE not paused
        if(gameManager.GetGameState() == GameState.ACTIVE && Mathf.Abs(transform.eulerAngles.y) <= angleCheck)
        {
            currentChargeTime += Time.deltaTime;
            if(currentChargeTime >= dialogueChargeTime)
            {
                Debug.Log("PlayerManager: Should be starting a dialogue because charge has been met");
                dialogueManager.StartDialogue();
            }
        }
    }

    public void ResetCharge()
    {
        currentChargeTime = 0;
    }
}
