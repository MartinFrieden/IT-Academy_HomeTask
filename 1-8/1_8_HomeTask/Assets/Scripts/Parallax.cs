using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Альтернативный вариант паралакса
public class Parallax : MonoBehaviour
{
    public GameObject[]     panels;
    public float            scrollSpeed;
    private float           panelWight;
    public float depth;
    // Start is called before the first frame update
    void Start()
    {
        panelWight = panels[0].transform.localScale.x;
        panels[0].transform.position = new Vector3(0,0,depth);
        panels[1].transform.position = new Vector3(panelWight, 0,depth);
        panels[2].transform.position = new Vector3(-panelWight, 0, depth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount>0 && Input.GetTouch(0).position.x > Screen.width / 2)
        {
            float tX, tY = 0;
            tX = Time.time * -scrollSpeed % panelWight + (panelWight * 0.5f);
            panels[0].transform.position = new Vector3(tX, tY, depth);
            if (tX >= 0)
            {
                panels[1].transform.position = new Vector3(tX - panelWight, tY,depth);
                panels[2].transform.position = new Vector3(tX + panelWight, tY, depth);
            }
            else
            {
                panels[1].transform.position = new Vector3(tX + panelWight, tY, depth);
                panels[2].transform.position = new Vector3(tX - panelWight, tY, depth);
            }
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).position.x < Screen.width / 2)
        {
            float tX, tY = 0;
            tX = Time.time * scrollSpeed % panelWight - (panelWight * 0.5f);

            panels[0].transform.position = new Vector3(tX, tY,depth);
            if (tX >= 0)
            {
                panels[1].transform.position = new Vector3(tX - panelWight, tY, depth);
                panels[2].transform.position = new Vector3(tX + panelWight, tY, depth);
            }
            else
            {
                panels[1].transform.position = new Vector3(tX + panelWight, tY, depth);
                panels[2].transform.position = new Vector3(tX - panelWight, tY, depth);
            }
        }
        
        
    }
}
