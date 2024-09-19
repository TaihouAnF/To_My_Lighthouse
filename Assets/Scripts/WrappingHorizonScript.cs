using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrappingHorizonScript : MonoBehaviour
{
    [Tooltip("The amount to scroll by")]
    [SerializeField]
    private float scrollAmount;

    private float scrollProgress;

    private PlayerManager playerManager;

    [Tooltip("The distance that the horizon should start from the player")]
    [SerializeField]
    private float horizonDistance;

    private float currentAngle = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        scrollProgress = 0;

        playerManager = FindObjectOfType<PlayerManager>();
    }

    public void ScrollHorizon(bool isRight)
    {

        if (!isRight)
        {
            scrollProgress += scrollAmount * Time.deltaTime;
        }
        else
        {
            scrollProgress -= scrollAmount * Time.deltaTime;
        }

        GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(scrollProgress, 0);
    }

    public void UpdateDistance(float distance)
    {
        Vector3 temp = transform.position;
        temp.z = distance;
        transform.position = temp;
    }
}
