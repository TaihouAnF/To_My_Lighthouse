using System.Collections;
using UnityEngine;

public class FaceBehavior : MonoBehaviour
{
    public float movingSpeed;
    public Transform bound_1;   // min x
    public Transform bound_2;   // max x
    public Transform bound_3;   // min y
    public Transform bound_4;   // max y
    private Vector3 targetPos;
    [SerializeField] private Transform dockingPos; // Used when question pops up
    private bool shouldGo;

    // Update is called once per frame
    void Update()
    {
        if (shouldGo)
        {
            // Move to the docking pos
            if (Vector3.Distance(transform.localPosition, dockingPos.localPosition) < 0.1f) { StartCoroutine(GoToDockingPos()); }
        }
        else
        {
            if (targetPos == Vector3.zero)
            {
                float minX = Mathf.Min(bound_1.localPosition.x, bound_2.localPosition.x);
                float minY = Mathf.Min(bound_3.localPosition.y, bound_4.localPosition.y);
                float minZ = Mathf.Min(bound_1.localPosition.z, bound_2.localPosition.z);
                float maxX = Mathf.Max(bound_1.localPosition.x, bound_2.localPosition.x);
                float maxY = Mathf.Max(bound_3.localPosition.y, bound_4.localPosition.y);
                float maxZ = Mathf.Max(bound_1.localPosition.z, bound_2.localPosition.z);
                float x = Random.Range(minX, maxX);
                float y = Random.Range(minY, maxY);
                float z = Random.Range(minZ, maxZ);
                targetPos = new Vector3(x, y, z);
                Debug.Log(targetPos);
            }
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, movingSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.localPosition, targetPos) < 0.1f) { targetPos = Vector3.zero; }
        }
    }

    private IEnumerator GoToDockingPos()
    {
        while (Vector3.Distance(transform.localPosition, dockingPos.localPosition) < 0.1f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, dockingPos.localPosition, 2 * movingSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void ResetMoving()
    {
        shouldGo = false;
    }
}
