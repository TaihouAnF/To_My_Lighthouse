using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanMotionScript : MonoBehaviour
{

    private float scrollProgress;

    [SerializeField]
    private Vector2 textureScale;

    [SerializeField]
    private float flowSpeed;

    private void Start()
    {
        scrollProgress = 0.0f;

        GetComponent<MeshRenderer>().material.mainTextureScale = textureScale;
    }

    // Update is called once per frame
    void Update()
    {

        if (FindObjectOfType<GameManager>().GetGameState() == GameState.ACTIVE)
        {

            scrollProgress += flowSpeed * Time.deltaTime;

            GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(scrollProgress, scrollProgress);
        }
    }
}
