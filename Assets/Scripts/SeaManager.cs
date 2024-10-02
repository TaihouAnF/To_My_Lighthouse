using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class SeaManager : MonoBehaviour
{

    [SerializeField]
    private GameObject gameSea;

    private Material seaMat;

    private PlayerManager playerManager;

    [SerializeField]
    private float maximumAngle;

    [SerializeField]
    private float maxAmplitude = 0.0f;

    [SerializeField]
    private float maxFrequency = 0.0f;

    [SerializeField]
    private float minAmplitude = 0.0f;

    [SerializeField]
    private float minFrequency = 0.0f;

    [SerializeField]
    private float minCamAmp;

    [SerializeField]
    private float maxCamAmp;

    [SerializeField]
    private Volume skyVolume;

    [SerializeField]
    private float minAerosolDensity;

    [SerializeField]
    private float maxAerosolDensity;

    [SerializeField]
    private Color minAerosolColor;

    [SerializeField]
    private Color maxAerosolColor;

    // Start is called before the first frame update
    void Start()
    {
        seaMat = gameSea.GetComponent<MeshRenderer>().material;
        playerManager = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerManager != null)
        {

            //Debug.Log(Vector3.Angle(Vector3.forward, playerManager.transform.forward));

            if (Vector3.Angle(Vector3.forward, playerManager.transform.forward) < maximumAngle)
            {
                UpdateSeaSettings(Vector3.Angle(Vector3.forward, playerManager.transform.forward));
                UpdateSkybox(Vector3.Angle(Vector3.forward, playerManager.transform.forward));
            }
        }
    }

    private void UpdateSeaSettings(float rotation)
    {

        float t = rotation / maximumAngle;
        float a = maxAmplitude * t;
        float f = maxFrequency * t;

        if(a < minAmplitude)
        {
            a = minAmplitude;
        }
        if(f < minFrequency)
        {
            f = minFrequency;
        }

        seaMat.SetFloat("_Blend", t);
        seaMat.SetFloat("_Frequency", f);
        seaMat.SetFloat("_Amplitude", a);

        float cA = t * maxCamAmp;
        if (cA < minCamAmp) 
        {
            cA = minCamAmp;
        }

        FindObjectOfType<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = cA;
    }

    private void UpdateSkybox(float rotation)
    {
        float t = rotation / maximumAngle;
        float d = maxAerosolDensity * t;
        Color c = Color.Lerp(minAerosolColor, maxAerosolColor, t);

        if(d < minAerosolDensity) { d = minAerosolDensity; }

        //FindObjectOfType<PhysicallyBasedSky>().aerosolTint.Override(c);
        //FindObjectOfType<PhysicallyBasedSky>().aerosolDensity.Override(d);

        VolumeProfile volProf = skyVolume.sharedProfile;
        if(volProf.TryGet<PhysicallyBasedSky>(out var sky))
        {
            sky.aerosolDensity.Override(d);
            sky.aerosolTint.Override(c);
        }
    }
}
