using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    public Animator animator;

    public void StartFading()
    {
        animator.SetTrigger("FadeOut");
    }

    public void ChangeScene(int index) 
    {
        SceneManager.LoadScene(index);
    }
}
