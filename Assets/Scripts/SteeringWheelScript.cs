using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheelScript : MonoBehaviour
{

    [Tooltip("Key for rotating left")]
    [SerializeField]
    private KeyCode left;

    [Tooltip("Key for rotating right")]
    [SerializeField]
    private KeyCode right;

    private GameManager gameManager;

    //Float for the amount of rotation the wheel should
    [SerializeField]
    private float rateOfRotation;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetGameState() != GameState.ACTIVE && gameManager.GetGameState() != GameState.ASKING && gameManager.GetGameState() != GameState.CHOOSING) 
            return;
        
        if(Input.GetKey(left))
        {
            transform.Rotate(Vector3.forward, rateOfRotation * Time.deltaTime);
        }
        else if(Input.GetKey(right))
        {
            transform.Rotate(Vector3.back, rateOfRotation * Time.deltaTime);
        }
    }
}
