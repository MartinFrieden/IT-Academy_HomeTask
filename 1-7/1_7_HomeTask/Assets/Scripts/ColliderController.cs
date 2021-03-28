using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
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
        //Debug.Log(other);
        if ((RoomSpawner.instance.currentCollider.transform.position.x - other.gameObject.transform.position.x) < 0)
        {
            RoomSpawner.instance.SpawnRoom(RoomSpawner.instance.top);
        }
        else
        {
            RoomSpawner.instance.SpawnRoom(RoomSpawner.instance.bottom);
        }

        if (RoomSpawner.instance.rooms[0] != null && RoomSpawner.instance.rooms[0] != RoomSpawner.instance.currentRoom)
        {
            Destroy(RoomSpawner.instance.rooms[0].gameObject);
            RoomSpawner.instance.rooms[0] = RoomSpawner.instance.currentRoom;
            RoomSpawner.instance.rooms[1] = RoomSpawner.instance.newRoom;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if ((RoomSpawner.instance.currentCollider.transform.position.x - other.gameObject.transform.position.x) > 0)
        {

            RoomSpawner.instance.SpawnRoom(RoomSpawner.instance.top);
        }
        else
        {
            RoomSpawner.instance.SpawnRoom(RoomSpawner.instance.bottom);
        }

        if (RoomSpawner.instance.rooms[0] != null && RoomSpawner.instance.rooms[0] != RoomSpawner.instance.currentRoom)
        {
            Destroy(RoomSpawner.instance.rooms[0].gameObject);
            RoomSpawner.instance.rooms[0] = RoomSpawner.instance.currentRoom;
            RoomSpawner.instance.rooms[1] = RoomSpawner.instance.newRoom;
        }
    }
}
