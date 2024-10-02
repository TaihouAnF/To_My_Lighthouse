using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

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
    private HDAdditionalLightData lightLight;

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

            //Debug.Log(hitInfo.distance);

            lightLight.intensity = Mathf.Lerp(maxIntensity, 0, t);

            float y = Mathf.Lerp(startHeight.y, minHeight.y, t);

            Vector3 temp = transform.position;
            temp.y = y;
            transform.position = temp;
        }
    }
}
