using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SwirlBehavior_Script : MonoBehaviour
{

    private Rigidbody rb;

    [SerializeField]
    private float impulseAmount;

    [SerializeField]
    private float bounceInterval;

    private bool isBouncin;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

       // StartCoroutine(BounceCharge());
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -3 && !isBouncin)
        {
            isBouncin = true;
            //StartCoroutine(BounceCharge());
            BounceSwirl();
        }
    }

    private void BounceSwirl()
    {
       // yield return new WaitForSeconds(bounceInterval);

        rb.AddForce(Vector3.up * impulseAmount, ForceMode.Impulse);

        //isBouncin = false;

        StartCoroutine(BounceCharge());
    }

    private IEnumerator BounceCharge()
    {
        yield return new WaitForSeconds(bounceInterval);

        isBouncin = false;
    }


}
