using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject particleTouch;

    public void OnCollisionEnter(Collision collision)
    {
        GameObject soundGO = AudioManager.instance.GetPooledObject(AudioManager.instance.BulletsSound);
        soundGO.transform.position = gameObject.transform.position;
        soundGO.GetComponent<SoundControl>().clipToPlayStart = AudioManager.instance.ballSound;
        soundGO.SetActive(true);
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
