using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



//НЕ ИСПОЛЬЗУЕТСЯ!!!!!!!
public class BackGroundController : MonoBehaviour
{
    public float speed;
    private RawImage image;
    float pos = 0;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).position.x > Screen.width / 2)
        {
            pos += speed * Time.deltaTime;
            if (pos > 1.0f)
            {
                pos -= 1.0f;
            }
            image.uvRect = new Rect(pos, 0, 1, 1);
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).position.x < Screen.width / 2)
        {
            pos -= speed * Time.deltaTime;
            if (pos > 1.0f)
            {
                pos -= 1.0f;
            }
            image.uvRect = new Rect(pos, 0, 1, 1);
        }

    }
}
