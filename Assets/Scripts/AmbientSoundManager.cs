using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundManager : MonoBehaviour
{
    public AudioSource source;
    public float volume;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!source.isPlaying) source.Play();
        source.volume = volume;
        source.loop = true;
        StartCoroutine(ChangeVolume());
    }

    private IEnumerator ChangeVolume()
    {
        float time = 0;
        while (time < 5)
        {
            time += Time.deltaTime;
            if (source.volume >= 1) break;
            source.volume = time / 5;
            yield return null;
        }
        source.volume = 1;
    }
}
