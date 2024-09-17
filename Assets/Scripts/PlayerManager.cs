using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Attributes")]
    public float playerRotateSpeed;
    private float horizontalInput;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdadePlayerRotation();
    }

    /// <summary>
    ///  Controlling rotation of the player.
    /// </summary>
    private void UpdadePlayerRotation() 
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, playerRotateSpeed * horizontalInput * Time.deltaTime);
    }
}
