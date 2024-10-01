using System.Collections;
using UnityEngine;

public class BackGoundController : MonoBehaviour
{
    public GameObject blanketLeft;
    public GameObject blanketRight;
    public GameObject backGround;
    public FadeController fadeController;
    [SerializeField] private float movingSpeed;

    public IEnumerator StartAnimation() {
        while (Vector3.Distance(blanketLeft.transform.position, backGround.transform.position) > 0.1f)
        {
            blanketLeft.transform.position = Vector3.MoveTowards(blanketLeft.transform.position, backGround.transform.position, movingSpeed * Time.deltaTime);
            blanketRight.transform.position = Vector3.MoveTowards(blanketRight.transform.position, backGround.transform.position, movingSpeed * Time.deltaTime);
            yield return null;
        }
        fadeController.StartFading();
    }

    public void Testing() 
    {
        StartCoroutine(StartAnimation());
    }
}
