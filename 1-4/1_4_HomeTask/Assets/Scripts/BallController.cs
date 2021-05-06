using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject particleTouch;

    public void OnCollisionEnter(Collision collision)
    {
        AudioManager.instance.PlaySound(AudioManager.Sounds.ballTouch, collision.GetContact(0).point);
        if (collision.gameObject.tag == "Wall")
        {
            Instantiate(particleTouch, this.gameObject.transform.position, collision.transform.rotation);
        }
    }

    private void OnEnable()
    {
        Rigidbody rigidB = gameObject.GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.zero;
    }

}
