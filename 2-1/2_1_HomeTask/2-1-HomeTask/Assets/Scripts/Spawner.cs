using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Singleton<Spawner>
{
    public Material cubeMaterial;
    public Vector3 width;
    public Vector3 height;
    public Vector3 length;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnCube();
        }
        transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);
    }

    public void SpawnCube()
    {
        GameObject cube = new GameObject();
        cube.transform.position = gameObject.transform.position;
        CubeGenerator makeCube = cube.AddComponent<CubeGenerator>();
        MeshFilter meshFilter = cube.GetComponent<MeshFilter>();
        SplitCube split = cube.AddComponent<SplitCube>();
        MeshRenderer meshRenderer = cube.GetComponent<MeshRenderer>();

        CombineInstance[] combineInstances = new CombineInstance[6];
        combineInstances[0].mesh = makeCube.Quad(new Vector3(0, -height.y/2, 0) - width / 2 - length / 2, length, width);
        combineInstances[1].mesh = makeCube.Quad(new Vector3(0, -height.y / 2, 0) - width / 2 - length / 2, width, height);
        combineInstances[2].mesh = makeCube.Quad(new Vector3(0, -height.y / 2, 0) - width / 2 - length / 2, height, length);
        combineInstances[3].mesh = makeCube.Quad(new Vector3(0, height.y / 2, 0) + width / 2 + length / 2, -width, -length);
        combineInstances[4].mesh = makeCube.Quad(new Vector3(0, height.y / 2, 0) + width / 2 + length / 2, -height, -width);
        combineInstances[5].mesh = makeCube.Quad(new Vector3(0, height.y / 2, 0) + width / 2 + length / 2, -length, -height);

        meshFilter.mesh.CombineMeshes(combineInstances, true, false);
        
        meshRenderer.material = cubeMaterial;
        cube.AddComponent<Rigidbody>();

        cube.AddComponent<MeshCollider>().convex = true;
    }

    //c - для противоположного угла при генерации куба

    public void SpawnCube(Vector3 pos, Vector3 newWidth, Vector3 newLength, int c, bool flipZ, bool FlipX)
    {
        GameObject cube = new GameObject();
        cube.transform.position = SplitCube.contactPoints[0].point;
        CubeGenerator makeCube = cube.AddComponent<CubeGenerator>();
        MeshFilter meshFilter = cube.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = cube.GetComponent<MeshRenderer>();

        CombineInstance[] combineInstances = new CombineInstance[6];
        combineInstances[0].mesh = makeCube.Quad(new Vector3(pos.x, pos.y, pos.z)*c, newLength*c, newWidth*c);
        combineInstances[1].mesh = makeCube.Quad(new Vector3(pos.x, pos.y, pos.z)*c, newWidth*c, height);
        combineInstances[2].mesh = makeCube.Quad(new Vector3(pos.x, pos.y, pos.z)*c, height, newLength*c);
        combineInstances[3].mesh = makeCube.Quad(new Vector3(pos.x+newWidth.x, pos.y+height.y*c, pos.z+newLength.z)*c, -newWidth*c, -newLength*c);
        combineInstances[4].mesh = makeCube.Quad(new Vector3(pos.x+newWidth.x, pos.y+height.y*c, pos.z+newLength.z)*c, -height, -newWidth*c);
        combineInstances[5].mesh = makeCube.Quad(new Vector3(pos.x+newWidth.x, pos.y+height.y*c, pos.z+newLength.z)*c, -newLength*c, -height);

        meshFilter.mesh.CombineMeshes(combineInstances, true, false);
        meshRenderer.material = cubeMaterial;

        if (flipZ)
        {
            cube.transform.localScale = new Vector3(cube.transform.localScale.x, cube.transform.localScale.y, -cube.transform.localScale.z);
        }
        if (FlipX)
        {
            cube.transform.localScale = new Vector3(-cube.transform.localScale.x, cube.transform.localScale.y, cube.transform.localScale.z);
        }
        cube.AddComponent<Rigidbody>();

        cube.AddComponent<MeshCollider>().convex = true;
    }
}
