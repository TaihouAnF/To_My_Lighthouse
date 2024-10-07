using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
