using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigController : MonoBehaviour
{
    public float strength; //стойкость свинки
    public float health;   //HP
    public int pigScore;   //очки за свинку
    Rigidbody2D _rigid;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > strength)
        {
            health -= collision.relativeVelocity.magnitude;
        }
        if(_rigid.velocity.magnitude > strength)
        {
            health -= _rigid.velocity.magnitude*0.5f;
        }
        if (health <= 0)
        {
            DestroyPig();
        }
    }

    void DestroyPig()
    {
        //добавить очки
        UIController.instance.AddScore(pigScore);
        //удалить свинку из списка
        GameManager.instance.pigs.Remove(this.gameObject);
        //проверить остались ли еще свинки
        GameManager.instance.PigsWatcher();
        Destroy(this.gameObject);
    }
}
