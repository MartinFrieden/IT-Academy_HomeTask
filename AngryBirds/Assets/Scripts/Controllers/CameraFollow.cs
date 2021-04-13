using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    static public GameObject target;    //объект слежения
    public float camPosZ;               //позиция камеры по z
    public float step = 0.05f;          //шаг для интерполяции камеры
    public Vector2 minCamPos = Vector2.zero;
    public GameObject startCameraPos;

    private Vector2 startTouchPos;
    private Camera _camera;

    void Awake()
    {
        camPosZ = this.transform.position.z;
        _camera = GetComponent<Camera>();
    }
    
    void FixedUpdate()
    {
        Vector3 targetPos;
        if (target == null)
        {
            targetPos = startCameraPos.transform.position;
            if (Input.GetMouseButtonDown(0)) startTouchPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            else if(Input.GetMouseButton(0))
            {
                float pos = _camera.ScreenToWorldPoint(Input.mousePosition).x - startTouchPos.x;
                transform.position = new Vector3(transform.position.x - pos, transform.position.y, transform.position.z);
            }
        }
        else
        {
            targetPos = target.transform.position;
            if (target.tag == "Bird")
            {
                if (target.GetComponent<Rigidbody2D>().IsSleeping())
                {
                    target = null;
                    return;
                }
            }
        }
        targetPos.x = Mathf.Max(minCamPos.x, targetPos.x);
        targetPos.y = Mathf.Max(minCamPos.y, targetPos.y);
        targetPos = Vector3.Lerp(transform.position, targetPos, step);
        targetPos.z = camPosZ;
        transform.position = targetPos;
        Camera.main.orthographicSize = targetPos.y + 5;
    }
}
