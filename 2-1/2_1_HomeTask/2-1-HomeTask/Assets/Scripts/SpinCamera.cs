using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float rotSpeed = 1.5f; 
    private float _rotY; 
    private Vector3 _offset;

    // Start is called before the first frame update
    void Start()
    {
        _rotY = transform.eulerAngles.y;
        _offset = target.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _rotY += rotSpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(10,_rotY,0);
        transform.position = target.position - (rotation * _offset);
        transform.LookAt(target);
    }


}
