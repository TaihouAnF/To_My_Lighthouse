using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float playerSpeed;
    private Vector2 dirc;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() 
    {
        UpdatePlayerMovement();
    }

    // Update is called once per frame
    void Update()
    {
        dirc.x = Input.GetAxis("Horizontal");
        dirc.y = Input.GetAxis("Vertical");
    }

    private void UpdatePlayerMovement() 
    {
        Vector2 pos = gameObject.transform.position;
        pos += dirc.normalized * playerSpeed * Time.deltaTime;
        rb.MovePosition(pos);
    }
}
