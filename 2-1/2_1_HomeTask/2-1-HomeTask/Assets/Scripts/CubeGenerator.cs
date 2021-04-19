using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CubeGenerator : MonoBehaviour
{
    public Mesh Quad(Vector3 startPoint, Vector3 width, Vector3 length)
    {
        Vector3 normal = Vector3.Cross(length, width).normalized;
        Mesh mesh = new Mesh
        {
            vertices = new[] { startPoint, startPoint + length, startPoint + length + width, startPoint + width },
            normals = new[] {normal, normal, normal, normal },
            triangles = new[] {0,1,2, 0,2,3 },
            uv = new[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) }
        };
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        return mesh;
    }  
}
