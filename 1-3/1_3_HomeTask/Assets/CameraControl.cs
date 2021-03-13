using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public void MoveCamera(Transform pos)
    {
        transform.position = pos.position;
        transform.rotation = pos.rotation;
    }
}
