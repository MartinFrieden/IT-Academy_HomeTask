using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SnapshotChange : MonoBehaviour
{
    public AudioMixerSnapshot inside;
    public AudioMixerSnapshot outside;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outside.TransitionTo(0.5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inside.TransitionTo(0.5f);
        }
    }
}
