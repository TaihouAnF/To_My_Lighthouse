using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DolphinBob_Behavior : MonoBehaviour
{

    [SerializeField]
    private float maxHeight;

    [SerializeField]
    private float minHeight;

    private float currentT;

    [SerializeField]
    private float maxT;

    [SerializeField]
    private float speed;

    private bool isUp;

    [SerializeField]
    private float maxDistance;

    // Start is called before the first frame update
    void Start()
    {
        currentT = 0;

        isUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isUp)
        {
            currentT += Time.deltaTime;
        }
        else
        {
            currentT -= Time.deltaTime;
        }

        if (currentT > maxT || currentT < 0)
        {
            isUp = !isUp;
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        LerpPosition();
    }

    private void LerpPosition()
    {
        float t = currentT / maxT;

        Vector3 newPos = transform.position;

        newPos.y = Mathf.Lerp(minHeight, maxHeight, t);

        transform.position = newPos;
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(transform.position, FindObjectOfType<PlayerManager>().transform.position);
        if (distance > maxDistance)
        {
            Vector3 pos = transform.position;
            pos.z = 0;
            transform.position = pos;
        }
    }
}
