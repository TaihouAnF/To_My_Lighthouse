using UnityEngine;

public class LightHouseManager : MonoBehaviour
{
    public float approchingSpeed;
    public float leavingSpeed;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 0.00001f) return;
        if (Input.GetKey(KeyCode.V))    // Getting Key is just for debugging
        {
            MoveAwayFromTargetAuto();   // Debug purpose
        }
        else 
        {
            MoveTowardsTargetAuto();    // Debug purpose
        }
    }

    /// <summary>
    /// Simply Moves the Object to the target automatically, Use for Debug Purpose.
    /// </summary>
    private void MoveTowardsTargetAuto() 
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        MoveTowardsTarget(distance);
    }

    /// <summary>
    /// Simply Moves the Object away from the target automatically, Use for Debug Purpose.
    /// </summary>
    private void MoveAwayFromTargetAuto() 
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        MoveAwayFromTarget(distance);
    }
    
    /// <summary>
    /// Moves the Object towards its target by a amount of distance.
    /// </summary>
    /// <param name="distance">The Amount for the Object to move.</param>
    public void MoveTowardsTarget(float distance) 
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Vector3 target = transform.position + direction * distance;
        if (Vector3.Distance(target, transform.position) < distance) return;
        transform.position = Vector3.MoveTowards(transform.position, target, approchingSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Moves the Object away from its target by a amount of distance.
    /// </summary>
    /// <param name="distance">The Amount for the Object to move.</param>
    public void MoveAwayFromTarget(float distance) 
    {
        Vector3 direction = (transform.position - player.transform.position).normalized;
        Vector3 target = transform.position + direction * distance;
        if (Vector3.Distance(target, transform.position) < distance) return;
        transform.position = Vector3.MoveTowards(transform.position, target, leavingSpeed * Time.deltaTime);
    }
}
