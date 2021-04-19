using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitCube : MonoBehaviour
{
    public static ContactPoint[] contactPoints;

    private void OnCollisionEnter(Collision collision)
    {
        contactPoints = collision.contacts;
        Debug.Log(contactPoints[0].point);

        Spawner.instance.SpawnCube(new Vector3(0, 0, 0), 
                                   new Vector3(gameObject.transform.position.x-contactPoints[0].point.x+Spawner.instance.width.x/2,0,0), 
                                   new Vector3(0,0,gameObject.transform.position.z-contactPoints[0].point.z+Spawner.instance.length.z/2), 1, false, false);
        Spawner.instance.SpawnCube(new Vector3(0, 0, 0), 
                                   new Vector3(Spawner.instance.width.x -(gameObject.transform.position.x - contactPoints[0].point.x + Spawner.instance.width.x / 2), 0, 0), 
                                   new Vector3(0, 0,Spawner.instance.length.z - (gameObject.transform.position.z - contactPoints[0].point.z + Spawner.instance.length.z / 2)), -1,false,false);

        Spawner.instance.SpawnCube(new Vector3(0, 0, 0), 
                                   new Vector3(Spawner.instance.width.x - (gameObject.transform.position.x - contactPoints[0].point.x + Spawner.instance.width.x / 2),0,0), 
                                   new Vector3(0,0, gameObject.transform.position.z - contactPoints[0].point.z + Spawner.instance.length.z / 2), 1,false,true);
        Spawner.instance.SpawnCube(new Vector3(0, 0, 0), 
                                   new Vector3(gameObject.transform.position.x - contactPoints[0].point.x + Spawner.instance.width.x / 2, 0, 0), 
                                   new Vector3(0,0,Spawner.instance.length.z - (gameObject.transform.position.z - contactPoints[0].point.z + Spawner.instance.length.z / 2)), -1,false,true);
        Destroy(gameObject);
    }
}
