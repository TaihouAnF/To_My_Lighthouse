using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChangeSceneById(int index)
    {
        SceneManager.LoadScene(index); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoBackMain()
    {
        ChangeSceneById(0);
    }
}
