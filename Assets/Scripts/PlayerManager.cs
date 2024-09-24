using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Attributes")]
    public float playerRotateSpeed;
    [SerializeField]
    private float vesselRotateSpeed;
    [Tooltip("Tells Whether the player is in control")]
    [SerializeField]
    private bool controlling;
    [SerializeField]
    private bool hasDirection;
    [SerializeField]
    private int direction;
    private readonly int[] dir = { -1, 1 };
    private float horizontalInput;

    [Tooltip("Abs of the angle that the player must be less than on the Y axis to count as charging since they'll be facing the lighthouse")]
    [SerializeField]
    private float angleCheck;

    [Tooltip("The value that determines in seconds how long the player has to look at the lighthouse before being prompted to answer a dialogue")]
    [SerializeField]
    private float dialogueChargeTime;
    private float currentChargeTime;
    public float moveCd;
    private float currMoveCd;

    private DialogueManager dialogueManager;
    private GameManager gameManager;
    private LightHouseManager lightHouseManager;
    private WrappingHorizonScript horizon;

    void Start()
    {

        currentChargeTime = 0;

        dialogueManager = FindObjectOfType<DialogueManager>();
        gameManager = FindObjectOfType<GameManager>();
        lightHouseManager = FindObjectOfType<LightHouseManager>();
        horizon = FindObjectOfType<WrappingHorizonScript>();

        controlling = false;
        hasDirection = false;

        currMoveCd = moveCd;
    }

    private void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0) 
        {
            controlling = true;
            hasDirection = false;
        }
        else if (!controlling && !hasDirection)
        {
            hasDirection = true;
            var rng = Random.Range(0, 2);
            direction = dir[rng];
        }
        else
        {
            controlling = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Only update the rotation if the game state is active
        if (gameManager.GetGameState() == GameState.ACTIVE)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            UpdatePlayerRotation();

            // CheckCharge();
        }

    }

    /// <summary>
    ///  Controlling rotation of the player.
    /// </summary>
    private void UpdatePlayerRotation() 
    {
        if (horizontalInput != 0)
        {
            Debug.Log("The player is Controlling.");
            float rotationAmount = horizontalInput * playerRotateSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotationAmount);
            currMoveCd = moveCd;
            // RotateVessel(rotationAmount);
        }
        else if (!controlling && currMoveCd <= 0)
        {
            Debug.Log("Now the vessel.");
            float rotationAmount = direction * vesselRotateSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotationAmount);
        }
        else 
        {
            Debug.Log("Cd");
            currMoveCd -= Time.deltaTime;
        }
    }

    private void CheckCharge()
    {
        //Only count the charge when the game state is ACTIVE not paused
        if(gameManager.GetGameState() == GameState.ACTIVE && (transform.eulerAngles.y <= angleCheck || transform.eulerAngles.y >= 360 - angleCheck))
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

    /// <summary>
    /// Rotate the vessel by a curtain amount.(Deprecated)
    /// </summary>
    /// <param name="rotationAmount">The Amount for the rotation.</param>
    // private void RotateVessel(float rotationAmount) 
    // {
    //     currRotation += rotationAmount;
    //     currRotation = Mathf.Clamp(currRotation, -10, 10);
    //     if ((currRotation == -10 && direction == -1) || (currRotation == 10 && direction == -1)) 
    //     {
    //         hasDirection = true;
    //         direction = -direction;
    //     }
    //     transform.rotation = Quaternion.Euler(0f, currRotation, 0f);
    // }
}
