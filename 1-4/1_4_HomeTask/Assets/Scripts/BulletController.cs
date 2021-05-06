using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public ParticleSystem effect;

    private void OnCollisionEnter(Collision collision)
    {

            AudioManager.instance.PlaySound(AudioManager.Sounds.bulletEnd, gameObject.transform.position);

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
