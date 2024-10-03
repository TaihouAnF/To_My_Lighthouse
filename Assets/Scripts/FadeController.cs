using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private SceneController sceneController;

    [SerializeField]
    private Color whiteOut;

    [SerializeField]
    private Image backgroundImage;

    public void StartFading()
    {
        animator.SetTrigger("FadeOut");
    }

    public void WhiteoutFade()
    {
        backgroundImage.color = whiteOut;

        animator.SetTrigger("FadeOut");
    }

    public void ChangeScene() 
    {
       sceneController.ChangeNextScene();
    }
}
