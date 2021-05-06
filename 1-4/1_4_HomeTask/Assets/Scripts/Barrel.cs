using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    float timer = 2f;
    private void Update()
    {
        timer -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {

        //foreach (var item in collision.contacts)
        //{
        if (collision.relativeVelocity.magnitude > 8 && timer < 1)
        {
            AudioManager.instance.PlaySound(AudioManager.Sounds.barrel, /*item.point*/ gameObject.transform.position);
            timer = 2;
        }      
            //}    
    }
}
