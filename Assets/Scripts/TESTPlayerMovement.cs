using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTPlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float rotateSpeed;

    [SerializeField]
    private GameObject sandObject;

    private float scrollAmount;

    void Start()
    {
        scrollAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.down * rotateSpeed);

            scrollAmount -= 1*Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * rotateSpeed);

            scrollAmount += 1*Time.deltaTime;
        }

        //sandObject.GetComponent<SpriteRenderer>().material.mainTextureOffset = new Vector2(scrollAmount, 0);
        //sandObject.GetComponent<SpriteRenderer>().material.SetVector("Sprite Texture", new Vector2(scrollAmount, 0));
        sandObject.GetComponent<MeshRenderer>().material.mainTextureOffset = new Vector2(scrollAmount, 0);

        Debug.Log(scrollAmount);
    }
}
