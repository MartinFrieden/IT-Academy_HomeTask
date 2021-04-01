using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxOffset : MonoBehaviour
{
    
    public float speed;
    Renderer rend;
    float offset;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).position.x > Screen.width / 2)
        {
            offset += Time.deltaTime * speed;
            rend.material.mainTextureOffset = new Vector2(offset, 0);
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).position.x < Screen.width / 2)
        {
            offset -= Time.deltaTime * speed;
            rend.material.mainTextureOffset = new Vector2(offset, 0);
        }
    }
}
