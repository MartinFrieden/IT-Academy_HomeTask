using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("OPEN!");
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y+3, transform.position.z);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("CLOSE!");
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);
        }
    }
}
