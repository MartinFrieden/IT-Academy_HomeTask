using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharController : MonoBehaviour
{
    public Button res;                          //кнопка воскрешения
    public float movementSpeed = 2.0f;          
    public float sprintSpeed = 5.0f;
    public float rotationSpeed = 0.2f;
    public float animationBlendSpeed = 0.2f;


    public float jumpSpeed = 7.0f;      //сила прыжка
    float speedY = 0.0f;                //для вертикальной скорости
    bool isJumping = false;             //в прыжке?


    CharacterController controller;
    Animator animator;
    Camera characterCamera;
    float rotationAngle = 0.0f;
    float targetAnimationSpeed = 0.0f;
    bool isSprint = false;
    DeatGravity dg;                     //действие гравитации после смерти
    
    int hitIndex = 0;
    float gravity = -9.81f;             //гравитация
   
    bool isDead = false;

    bool isHit = false;
    bool isSpawn = false;

    public CharacterController Controller
    {
        get { return controller = controller ?? GetComponent<CharacterController>(); }
    }

    public Camera CharacterCamera
    {
        get { return characterCamera = characterCamera ?? FindObjectOfType<Camera>(); }
    }

    public Animator CharacterAnimator
    {
        get { return animator = animator ?? GetComponent<Animator>(); }
    }

    void Start()
    {
        //гравитация после смерти (отключить)
        dg = this.gameObject.GetComponent<DeatGravity>();
        dg.enabled = false;

        //отключаем кнопку и курсор
        res.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //проигрываем анимацию спауна
        CharacterAnimator.Play("EllenSpawn");
    }

    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        //случайная атака
        if (Input.GetMouseButtonDown(0) && !isSpawn && !isJumping && !isDead)
        {
            isHit = true;
            hitIndex = Random.Range(0,5);
            CharacterAnimator.SetInteger("HitIndex", hitIndex);
            CharacterAnimator.SetTrigger("Hit");
        }

        //потеряла сознание
        if (Input.GetKeyDown(KeyCode.E))
        {
            Death();
        }

        //прыжок
        if (Input.GetButtonDown("Jump") && !isJumping && !isHit)
        {
            CharacterAnimator.applyRootMotion = false;
            isJumping = true;
            CharacterAnimator.SetTrigger("Jump");
            speedY += jumpSpeed;
        }
        if (!Controller.isGrounded)
        {
            speedY += gravity * Time.deltaTime;
        }
        else if(speedY < 0.0f)
        {
            CharacterAnimator.applyRootMotion = true;
            speedY = 0.0f;
        }

        CharacterAnimator.SetFloat("SpeedY", speedY / jumpSpeed);

        //определение приземления
        if (isJumping && speedY < 0.0f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f, LayerMask.GetMask("Default")))
            {
                isJumping = false;
                CharacterAnimator.SetTrigger("Land");
            }
        }

        //спринт
        isSprint = Input.GetKey(KeyCode.LeftShift);

        Vector3 movement = new Vector3(horizontal, 0.0f, vertical);
        Vector3 rotatedMovement = Quaternion.Euler(0.0f, CharacterCamera.transform.rotation.eulerAngles.y, 0.0f) * movement.normalized;
        Vector3 verticalMovement = Vector3.up * speedY;

        float currentSpeed = isSprint ? sprintSpeed : movementSpeed;

        //ограничение перемещения при респуне смерти и атаке
        if (!isHit && !isSpawn && !isDead)
        {
            Controller.Move((verticalMovement + rotatedMovement * currentSpeed) * Time.deltaTime);
        }

        //угол поворота
        if (rotatedMovement.sqrMagnitude > 0.0f)
        {
            rotationAngle = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
            targetAnimationSpeed = isSprint ? 1.0f : 0.5f;
        }
        else
        {
            targetAnimationSpeed = 0.0f;
        }

        CharacterAnimator.SetFloat("Speed", Mathf.Lerp(CharacterAnimator.GetFloat("Speed"), targetAnimationSpeed, animationBlendSpeed));
        Quaternion currentRotation = Controller.transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);

        //ограничение вращения при смерти
        if (!isDead && !isSpawn)
        {
            Controller.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed);
        }
    }

    //смерть
    public void Death()
    {
        isJumping = false;
        isDead = true;
        dg.enabled = true;  //действие гравитации после смерти (включаю вспомогательный скрипт)
        CharacterAnimator.SetTrigger("Death");
        StartCoroutine(WaitingAfterDeath());   //после некоторого времени появляется кнопка воскрешения    
    }

    //показ кнопки воскрешения
    void ShowButton()
    {
        res.gameObject.SetActive(true); //активировать кнопку
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    //задержка после смерти
    IEnumerator WaitingAfterDeath()
    {
        yield return new WaitForSeconds(1.5f);
        ShowButton();       //показать кнопку воскрешения
    }

    public void Respawn()
    {
        dg.enabled = false; //отключаем гравитацию после смерти
        res.gameObject.SetActive(false); //отключаем кнопку воскрешения
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CharacterAnimator.Play("EllenSpawn");
        isDead = false;
        isHit = false;
    }



    //Animation Events
    public void MeleeAttackStart()
    {
        isHit = true;
    }

    public void MeleeAttackEnd()
    {
        isHit = false;
    }

    public void EllenSpawnStart()
    {
        isSpawn = true;
    }

    public void EllenSpawnEnd()
    {
        isSpawn = false;
        isJumping = false;
    }

    public void HardLandingStart()
    {
        isHit = true;
    }

    public void HardLandingEnd()
    {
        isHit = false;
    }
}
