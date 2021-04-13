using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public float strength;
    public float health;
    Rigidbody2D _rigid;
    // Start is called before the first frame update
    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > strength)
        {
            health -= collision.relativeVelocity.magnitude;
        }

        if (_rigid.velocity.magnitude > strength)
        {
            health -= _rigid.velocity.magnitude;
        }
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
