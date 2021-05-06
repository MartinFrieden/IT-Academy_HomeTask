using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEvent : MonoBehaviour
{
    public event Action<GameObject> OnRelease;
    public float timeToRelease;

    private void OnEnable()
    {
        StartCoroutine(StartRelease());
    }

    IEnumerator StartRelease()
    {
        yield return new WaitForSeconds(timeToRelease);
        OnRelease?.Invoke(gameObject);
    }
}
