using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class SoundControl : MonoBehaviour
{
    public float releaseTime = 1;
    public AudioClip clipToPlayStart { get; set; }
    AudioSource source;

    private void OnEnable()
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(clipToPlayStart);
        Invoke("Release", releaseTime);
    }

    void Release()
    {
        gameObject.SetActive(false);
    }
}
