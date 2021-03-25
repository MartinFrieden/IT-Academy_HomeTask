using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeatGravity : MonoBehaviour
{
    float gravity = -9.8f;
    CharacterController controller;
    float speedY = 0.0f;

    public CharacterController Controller
    {
        get { return controller = controller ?? GetComponent<CharacterController>(); }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Controller.isGrounded)
        {
            speedY += gravity * Time.deltaTime;
        }
        Vector3 verticalMovement = Vector3.up * speedY;
        Controller.Move(verticalMovement * Time.deltaTime);
    }
}
