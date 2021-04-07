using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float maxSpeed = 50f;
    private bool isFacingRight = true;
    public LayerMask floorMask;
    Rigidbody2D _rigid;
    RaycastHit2D groundCast;

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _rigid.velocity = new Vector2(maxSpeed * Time.deltaTime, _rigid.velocity.y);
        if (!CheckGround(transform.position))
        {
            Flip();
        }
    }

    bool CheckGround(Vector2 pos)
    {
        Vector2 leftCast = new Vector2(pos.x-1f,pos.y-0.5f);
        Vector2 rightCast = new Vector2(pos.x + 1f, pos.y - 0.5f);

        if(!isFacingRight)
            groundCast = Physics2D.Raycast(leftCast, Vector2.down, .2f, floorMask);
        else
            groundCast = Physics2D.Raycast(rightCast, Vector2.down, .2f, floorMask);

        if (groundCast.collider == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }
 

    private void OnBecameVisible()
    {
        enabled = true;
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        maxSpeed *= -1;
    }
   
}
