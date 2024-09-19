using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Tooltip("Index in the build settings of the scene to load into")]
    [SerializeField]
    private int sceneIndex;
    
    public void OnClick()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
