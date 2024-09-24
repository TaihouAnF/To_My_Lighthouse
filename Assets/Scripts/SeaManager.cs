using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaManager : MonoBehaviour
{

    [SerializeField]
    private GameObject gameSea;

    private Material seaMat;

    private PlayerManager playerManager;

    [SerializeField]
    private float maximumAngle;

    // Start is called before the first frame update
    void Start()
    {
        seaMat = gameSea.GetComponent<MeshRenderer>().material;
        playerManager = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerManager != null)
        {

            //Debug.Log(Vector3.Angle(Vector3.forward, playerManager.transform.forward));

            if (Vector3.Angle(Vector3.forward, playerManager.transform.forward) < maximumAngle)
            {
                UpdateSeaSettings(Vector3.Angle(Vector3.forward, playerManager.transform.forward));
            }
        }
    }

    private void UpdateSeaSettings(float rotation)
    {

        float t = rotation / maximumAngle;

        seaMat.SetFloat("_Blend", t);
    }
}
