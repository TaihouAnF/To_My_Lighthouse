using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsDoneButton : MonoBehaviour
{
    [SerializeField]
    private Canvas settingsCanvas;

    [SerializeField]
    private Canvas gameCanvas;

    public void SettingsCompleteOnClick()
    {
        gameCanvas.gameObject.SetActive(true);
        settingsCanvas.gameObject.SetActive(false);
    }
}
