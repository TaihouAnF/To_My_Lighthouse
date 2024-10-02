using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeController : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private SceneController sceneController;

    public void StartFading()
    {
        animator.SetTrigger("FadeOut");
    }

    public void ChangeScene() 
    {
       sceneController.ChangeNextScene();
    }
}
