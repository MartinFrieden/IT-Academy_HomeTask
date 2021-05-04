using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEvent : MonoBehaviour
{
    public event Action<GameObject> OnRelease;
    public float timeToRelease;

    private void OnCollisionEnter(Collision collision)
    {
        foreach (var item in OnRelease.GetInvocationList())
        {
            Debug.Log("Podpisota " + item);
        }
        StartCoroutine(StartRelease());
    }

    IEnumerator StartRelease()
    {
        yield return new WaitForSeconds(timeToRelease);
        OnRelease?.Invoke(gameObject);
    }
}
