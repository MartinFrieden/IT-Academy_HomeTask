using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var particles = GetComponent<ParticleSystem>();
        particles.loop = false;
        particles.Play();
        Destroy(this.gameObject, particles.main.duration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
