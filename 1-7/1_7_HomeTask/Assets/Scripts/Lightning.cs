using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    Light lightning;
    public List <AudioClip> lightningSounds;

    public float minTime = 0.2f;
    public float treashHold = 0.9f;
    float lastTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        lightning = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - lastTime) > minTime)
        {
            if (Random.value > treashHold)
            {
                var audio = GetComponent<AudioSource>();
                if (audio)
                {
                    audio.PlayOneShot(lightningSounds[Random.Range(0, lightningSounds.Count-1)]);
                }
                lightning.enabled = true;
            }
            else
            {
                lightning.enabled = false;
            }
            lastTime = Time.time;
        }
        
    }

}
