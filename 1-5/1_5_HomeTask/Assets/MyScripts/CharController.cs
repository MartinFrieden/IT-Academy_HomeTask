using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/CharController")]

public class CharController : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float rotSpeed = 15.0f;
    public float gravity = -9.8f;
    public float moveSpeed = 6f;
    public float jumpSpeed = 15.0f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;
    private float _vertSpeed;

    CharacterController controller;
    public CharacterController Controller { get { return controller = controller ?? GetComponent<CharacterController>(); } }
    public bool isJump { get; set; }

    Vector2 startTouch;
    Vector2 deltaPos;
    Touch touch;

    private Animator anim;

    int currentTouchID = -1;

    private void Start()
    {
        _vertSpeed = minFall;
        anim = this.gameObject.GetComponent<Animator>();
        Input.multiTouchEnabled = true;
    }


    void Update()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                var touchItem = Input.GetTouch(i);
                if (touchItem.phase == TouchPhase.Began && touchItem.position.x < Screen.width / 2)
                {
                    currentTouchID = i;
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

            if (touch.position.x < Screen.width / 2)
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

                if (touch.phase == TouchPhase.Ended)
                {
                    startTouch = Vector2.zero;
                    deltaPos = Vector2.zero;
                    currentTouchID = -1;
                }

            }
        }

        float deltaX = -deltaPos.x * moveSpeed;
        float deltaZ = -deltaPos.y * moveSpeed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, moveSpeed);
        movement.y = gravity;

        if (Controller.isGrounded)
        {
            anim.SetBool("Jumping", false);
            if (isJump)
            {
                _vertSpeed = jumpSpeed;
            }
            else
            {
                _vertSpeed = minFall;
            }
        }
        else
        {
            anim.SetBool("Jumping", true);
            _vertSpeed += gravity * 5 * Time.deltaTime;
            if (_vertSpeed < terminalVelocity)
            {
                _vertSpeed = terminalVelocity;
            }
        }
        movement.y = _vertSpeed;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        Controller.Move(movement);


        if (deltaPos != Vector2.zero)
            anim.SetBool("Idling", true);
        else
            anim.SetBool("Idling", false);

        anim.SetFloat("MoveType", -deltaPos.y);
    } 
}
