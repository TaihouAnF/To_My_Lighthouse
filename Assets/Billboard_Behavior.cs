using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard_Behavior : MonoBehaviour
{
    // Start is called before the first frame update

    private PlayerManager playerManager;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerManager.transform.position, Vector3.up);
    }
}
