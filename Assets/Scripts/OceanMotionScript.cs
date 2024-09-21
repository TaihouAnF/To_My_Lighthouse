using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanMotionScript : MonoBehaviour
{

    private float scrollProgress;

    [SerializeField]
    private Material calmMaterial;

    [SerializeField]
    private Material roughMaterial;

    private MeshRenderer meshRenderer;

    [SerializeField]
    private Vector2 textureScale;

    [SerializeField]
    private float flowSpeed;

    [SerializeField]
    [Range(0f, 1f)]
    private float roughness;

    private void Start()
    {
        scrollProgress = 0.0f;

        meshRenderer = GetComponent<MeshRenderer>();
        
        //meshRenderer.material.Lerp(calmMaterial, roughMaterial, roughness);

        meshRenderer.material.mainTextureScale = textureScale;

        //GetComponent<MeshRenderer>().material.mainTextureScale = textureScale;
    }

    // Update is called once per frame
    void Update()
    {

        if (FindObjectOfType<GameManager>().GetGameState() == GameState.ACTIVE)
        {

            scrollProgress += flowSpeed * Time.deltaTime;

            GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(scrollProgress, scrollProgress);
        }

        //meshRenderer.material.Lerp(calmMaterial, roughMaterial, roughness);

        //meshRenderer.material.mainTextureScale = textureScale;
    }

    public void UpdateRoughness(float t)
    {
        roughness = t;
    }
}
