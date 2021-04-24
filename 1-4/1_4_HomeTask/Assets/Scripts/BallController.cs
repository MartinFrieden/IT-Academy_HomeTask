using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameObject particleTouch;



    public void OnCollisionEnter(Collision collision)
    {
            if (collision.gameObject.tag == "Wall")
            {
            Instantiate(particleTouch, this.gameObject.transform.position, collision.transform.rotation);
        }
    }
}
