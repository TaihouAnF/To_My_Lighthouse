using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBounce_Behavior : MonoBehaviour
{

    [SerializeField]
    private float maxDelay;

    [SerializeField]
    private float minDelay;

    [SerializeField]
    private float cloudSpeed;

    private bool isLeft;

    // Start is called before the first frame update
    void Start()
    {
        float currentDelay = Random.Range(minDelay, maxDelay);

        isLeft = false;

        StartCoroutine(SwitchDirection(currentDelay));
    }

    // Update is called once per frame
    void Update()
    {
        if(isLeft)
        {
            transform.Translate(Vector3.left * cloudSpeed *Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * cloudSpeed * Time.deltaTime);
        }
    }

    private IEnumerator SwitchDirection(float delay)
    {
        yield return new WaitForSeconds(delay);

        isLeft = !isLeft;

        float newDelay = Random.Range(minDelay, maxDelay);

        StartCoroutine(SwitchDirection(newDelay));
    }
}
