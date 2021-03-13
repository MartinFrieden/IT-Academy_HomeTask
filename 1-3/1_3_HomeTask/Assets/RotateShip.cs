using UnityEngine;

public class RotateShip : MonoBehaviour
{
    float m_y;
    void Update()
    {
        if (Input.touchCount>0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                if(touch.deltaPosition.x > 0)
                    m_y = -250 * Time.deltaTime;
                else
                    m_y = 250 * Time.deltaTime;
                transform.Rotate(Vector3.up, m_y, Space.Self);
            }
        }
    }
}
