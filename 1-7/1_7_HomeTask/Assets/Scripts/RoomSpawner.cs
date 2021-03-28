using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : Singleton<RoomSpawner>
{
    public GameObject room;
    public GameObject player;
    public GameObject currentRoom;
    public GameObject newRoom;
    public Transform top, bottom;
    public Collider currentCollider;
    public List<GameObject> rooms;
    
    void Start()
    {
        rooms = new List<GameObject>();
    }

    public void SpawnRoom(Transform spawnPosition)
    {
        //спаунить комнaту в Position

        if (newRoom != null && newRoom != currentRoom)
        {
            Destroy(newRoom.gameObject);
            rooms.RemoveAt(1);
        }


        newRoom = Instantiate(room, spawnPosition.position, Quaternion.identity);
        rooms.Add(newRoom);
    }
}
