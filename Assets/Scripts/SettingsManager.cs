using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{

    private bool motionDisabled;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        motionDisabled = false;
    }

    public void ToggleMotionSetting()
    {
        motionDisabled = !motionDisabled;
    }

    public bool GetMotionSetting()
    {
        return motionDisabled;
    }

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
