using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector2 deltaPos;
    private Vector2 startTouch;
    public enum RotationAxes
    {
        RotXY = 0,
        RotX = 1,
        RotY = 2
    }

    public RotationAxes axes = RotationAxes.RotXY;
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    public float minVert = -45.0f;
    public float maxVert = 45.0f;
    private float _rotationX = 0;
    int currentTouchID = -1;
    Touch touch;
    void Start()
    {
        Input.multiTouchEnabled = true;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                var touchItem = Input.GetTouch(i);
                if(touchItem.phase == TouchPhase.Began && touchItem.position.x > Screen.width / 2)
                {
                    currentTouchID = touchItem.fingerId;
                }
            }

            foreach (var item in Input.touches)
            {
                if (item.fingerId == currentTouchID)
                {
                    touch = item;
                    break;
                }
            }

            if (touch.position.x > Screen.width/2)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    startTouch = touch.position;
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    deltaPos = startTouch - touch.position;
                    deltaPos = Vector3.ClampMagnitude(deltaPos, 1);
                }

                if (touch.phase == TouchPhase.Stationary)
                {
                    startTouch = touch.position;
                    deltaPos = Vector2.zero;

                }

                if (touch.phase == TouchPhase.Ended)
                {
                    startTouch = Vector2.zero;
                    deltaPos = Vector2.zero;
                    currentTouchID = -1;
                }

            }
        }

        if (axes == RotationAxes.RotX)
        {
            transform.Rotate(0, -deltaPos.x*sensitivityHor, 0);
        }
        else if (axes == RotationAxes.RotY)
        {
            _rotationX += deltaPos.y * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);
            float rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        else
        {
            _rotationX += deltaPos.y * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);

            float delta = -deltaPos.x * sensitivityHor;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }

    }


}
