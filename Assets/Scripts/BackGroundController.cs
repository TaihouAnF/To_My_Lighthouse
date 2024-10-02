using System.Collections;
using UnityEngine;

public class BackGoundController : MonoBehaviour
{
    public GameObject blanketLeft;
    public GameObject blanketRight;
    public GameObject backGround;
    public FadeController fadeController;
    [SerializeField] private float movingSpeed;

    [SerializeField] private float cooldown;
    // private float actualCd;

    void Start()
    {
        StartCoroutine(StartAnimation(cooldown));
    }

    // void Update()
    // {
        
    //     if (actualCd >= cooldown) 
    //     {
    //         Debug.Log("now");
    //         // StartCoroutine(StartAnimation());
    //     }
    //     else 
    //     {
    //         actualCd += Time.deltaTime;
    //         Debug.Log(actualCd);
    //     }
    // }

    public IEnumerator StartAnimation(float amount) {
        yield return new WaitForSeconds(amount);
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
        Debug.Log("SHOULD GO");
        StartCoroutine(StartAnimation(0));
    }
}
