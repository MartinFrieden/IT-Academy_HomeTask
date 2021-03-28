using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Lantern : MonoBehaviour
{
    Light lanternLight;
    public AudioClip clickAudio; 
    void Start()
    {
        lanternLight = gameObject.GetComponent<Light>();
        lanternLight.enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SwithchLight();
        }
    }

    void SwithchLight()
    {
        lanternLight.enabled = !lanternLight.enabled;
        var audio = GetComponent<AudioSource>();
        if (audio)
        {
            audio.PlayOneShot(this.clickAudio);
        }

    }
}
