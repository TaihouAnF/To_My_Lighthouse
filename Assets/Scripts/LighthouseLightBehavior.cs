using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighthouseLightBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject lighthouse;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float maxIntensity;

    [SerializeField]
    private Light lightLight;

    [SerializeField]
    private float startingDistance;

    [SerializeField]
    private Vector3 minHeight;

    private Vector3 startHeight;

    void Start()
    {
        startHeight = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Linecast(player.transform.position, lighthouse.transform.position, out RaycastHit hitInfo))
        {
            float t = hitInfo.distance / startingDistance;

            Debug.Log(hitInfo.distance);

            lightLight.intensity = Mathf.Lerp(0, maxIntensity, t);

            transform.position = Vector3.Lerp(startHeight, minHeight, t);
        }
    }
}
