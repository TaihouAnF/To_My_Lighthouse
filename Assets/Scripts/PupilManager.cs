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
        Debug.Log(playerFacing);
        float movingAmount = playerFacing.x * maxX; 
        transform.position = defaultPosition + new Vector3(movingAmount, (float)(-Mathf.Cos(movingAmount / maxX) + 0.25), 0);
    }
}
