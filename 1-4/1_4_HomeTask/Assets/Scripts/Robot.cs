using UnityEngine;

public class Robot : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;

    private Animator anim;

    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * movementSpeed; 
        float deltaZ = Input.GetAxis("Vertical") * movementSpeed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        //движение по диагонали с заданной скоростью
        movement = Vector3.ClampMagnitude(movement, movementSpeed);
        transform.Translate(movement);

        //анимация
        if (deltaX != 0 || deltaZ != 0)
        {
            anim.SetInteger("Speed", 2);
        }
        else
            anim.SetInteger("Speed", 0);
    }
}
