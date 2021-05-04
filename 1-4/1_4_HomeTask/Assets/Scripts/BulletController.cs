using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public ParticleSystem effect;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject soundGO = AudioManager.instance.GetPooledObject(AudioManager.instance.BulletsSound);
        soundGO.transform.position = gameObject.transform.position;
        soundGO.GetComponent<SoundControl>().clipToPlayStart = AudioManager.instance.bulletEndSound;
        soundGO.SetActive(true);
        gameObject.GetComponent<Collider>().enabled = false;
        effect.Play(true);
    }

    private void OnEnable()
    {
        gameObject.GetComponent<Collider>().enabled = true;
        effect.Play(true);
        Rigidbody rigidB = gameObject.GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.zero;
    }
}
