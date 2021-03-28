using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]

public class FPSInput : MonoBehaviour
{
   
    public Transform startPos;
    public float speed = 6.0f;
    public float gravity = -9.8f;
    public float jumpSpeed = 15.0f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;
    private float _vertSpeed;
    public AudioClip stepSound;
    AudioSource audioS;
    private CharacterController _charController;
    CharacterController Controller
    {
        get { return _charController = _charController ?? GetComponent<CharacterController>(); }
    }

    // Start is called before the first frame update
    void Start()
    {
        _vertSpeed = minFall;
        Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;
        audioS = GetComponent<AudioSource>();
        audioS.clip = stepSound;
        audioS.playOnAwake = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed; 
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);

        if (Controller.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                _vertSpeed = jumpSpeed;
            }
            else
            {
                _vertSpeed = minFall;
            }
            if (Controller.velocity.sqrMagnitude > 0)
            {
                if(!audioS.isPlaying)
                    audioS.Play();
            }
            else if(audioS.isPlaying)
            {
                audioS.Stop();
            }
        }
        else
        {
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
        
    }
}
