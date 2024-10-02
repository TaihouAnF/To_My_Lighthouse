using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class LightHouseManager : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public bool movingTriggered;
    public bool inMotion;
    public float duration = 5f;
    private float distance;
    private Vector3 direction;

    [SerializeField]
    private TextMeshPro tmp;

    void Start() {
        movingTriggered = false;
        inMotion = false;

        distance = 0f;
        direction = (player.transform.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingTriggered && !inMotion) 
        {
            StartCoroutine(MoveLightHouse());
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
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
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
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    /// <summary>
    /// A method to tell the lighthouse should move.
    /// </summary>
    /// <param name="dist">The distance the lighthouse should be moving</param>
    /// <param name="towards">The direction of moving, True means move towards and False means move away.</param>
    public void ShouldMove(float dist, bool towards)
    {
        movingTriggered = true;
        distance = dist;
        direction = towards ? (player.transform.position - transform.position).normalized :
                              (transform.position - player.transform.position).normalized;
    }

    /// <summary>
    /// Move the Lighthouse to the target in a duration time.
    /// </summary>
    public IEnumerator MoveLightHouse()
    {
        inMotion = true;
        Vector3 target = transform.position + distance * direction;
        float timeElapsed = 0;
        while (timeElapsed < duration) 
        {
            float t = timeElapsed / duration;
            transform.position = Vector3.Lerp(transform.position, target, t);
            FindObjectOfType<WrappingHorizonScript>().UpdateDistance(transform.position.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = target;
        inMotion = false;
        movingTriggered = false;
    }

    public void SetText(string text)
    {
        if (text == null) return;

        tmp.text = text;
    }
}
