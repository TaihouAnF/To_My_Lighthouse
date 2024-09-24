using UnityEngine;

public class PupilManager : MonoBehaviour
{
    public GameObject player;
    private Vector3 defaultPosition;
    public float maxX;

    void Start() 
    {
        defaultPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerFacing = player.transform.forward;
        UpdatePupilPosition(playerFacing);
    }

    private void UpdatePupilPosition(Vector3 playerFacing) 
    {
        float movingAmount = playerFacing.x * maxX;     // Should be good without clamp as we times maxX and facing.x = [-1, 1].
        transform.position = defaultPosition + new Vector3(movingAmount, 0, 0);
    }
}
