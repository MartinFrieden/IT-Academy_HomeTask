using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
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

        RoomSpawner.instance.currentRoom = gameObject;
        RoomSpawner.instance.currentCollider = gameObject.transform.Find("currentCollider").GetComponent<Collider>();
        RoomSpawner.instance.top = gameObject.transform.Find("Top").GetComponent<Transform>();
        RoomSpawner.instance.bottom = gameObject.transform.Find("Bottom").GetComponent<Transform>();
        if (RoomSpawner.instance.rooms.Count == 0)
        {
            RoomSpawner.instance.rooms.Add(RoomSpawner.instance.currentRoom);
            RoomSpawner.instance.SpawnRoom(RoomSpawner.instance.bottom);
        }
    }
}
