using UnityEngine;

public class FaceBehavior : MonoBehaviour
{
    public float movingSpeed;
    public float maxX;
    public float maxY;
    public float minX;
    public float minY;
    private Vector3 targetPos;
    [SerializeField] private Transform dockingPos; // Used when question pops up
    private bool shouldGo;

    // Update is called once per frame
    void Update()
    {

        if (shouldGo)
        {
            // Move to the docking pos
            if (Vector3.Distance(transform.localPosition, dockingPos.localPosition) > 0.1f) 
            { 
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, dockingPos.localPosition, 2 * movingSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (targetPos == Vector3.zero)
            {
                float x = Random.Range(minX, maxX);
                float y = Random.Range(minY, maxY);
                targetPos = new Vector3(x, y, transform.localPosition.z);
            }
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, movingSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.localPosition, targetPos) < 0.1f) { targetPos = Vector3.zero; }
        }
    }

    // private IEnumerator GoToDockingPos()
    // {
    //     while (Vector3.Distance(transform.localPosition, dockingPos.localPosition) < 0.1f)
    //     {
    //         transform.localPosition = Vector3.MoveTowards(transform.localPosition, dockingPos.localPosition, 2 * movingSpeed * Time.deltaTime);
    //         yield return null;
    //     }
    // }

    public void ResetMoving()
    {
        shouldGo = false;
    }

    public void SetMoving()
    {
        shouldGo = true;
    }
}
