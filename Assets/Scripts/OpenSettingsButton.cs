using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSettingsButton : MonoBehaviour
{
    [SerializeField]
    private Canvas settingsCanvas;

    [SerializeField]
    private Canvas gameCanvas;

    public void OpenSettingsOnClick()
    {
        settingsCanvas.gameObject.SetActive(true);
        gameCanvas.gameObject.SetActive(false);
    }
}
