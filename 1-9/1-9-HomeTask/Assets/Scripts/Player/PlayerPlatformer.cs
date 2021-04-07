using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformer : MonoBehaviour
{
    public float speed = 250.0f;
    private Rigidbody2D _body;
    private Collider2D _coll;
    public float jumpForce = 12.0f;
    bool isFacingRight = true;
  



    public int Direction { get; set; }
    public bool IsJump { get; set; }
    float deltaX;
    bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        deltaX = Direction * speed * Time.deltaTime;
        Vector2 movement = new Vector2(deltaX, _body.velocity.y);
        if (movement.x < 0 && isFacingRight)
        {
            Flip();
        }
        else if(movement.x > 0 && !isFacingRight)
        {
            Flip();
        }
        _body.velocity = movement;
    }

    private void Update()
    {

        if (Physics2D.Raycast(transform.position, Vector2.down, _coll.transform.localScale.y / 2 + 0.2f, LayerMask.GetMask("Default")))
        {
            if (_body.velocity.y <= 0)
                grounded = true;    
        }
        //_body.gravityScale = grounded && deltaX == 0 ? 0 : 1;
        if (grounded && IsJump)
        {
            _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            grounded = false;
        }

        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        MovingPlatform platform = null;
        if (collision.gameObject.tag == "MovingPlatform")
        {
            platform = collision.gameObject.GetComponent<MovingPlatform>();
            if (platform != null)
            {
                transform.parent = platform.transform;
            }
        }
        else
        {
            transform.parent = null;
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        EnemyController enemy = collision.collider.GetComponent<EnemyController>();
        Block block = collision.collider.GetComponent<Block>();
        if (enemy != null)
        {
            foreach(ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y >= 0.6f)
                {
                    enemy.DestroyEnemy();
                }
                else
                {
                    GameManager.instance.showButton();
                    Death();
                }
            }
        }

        if (block != null)
        {
            foreach(ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y <= 0.6)
                {
                    block.BlockBounce();
                }
            }
        }

        if (collision.gameObject.tag == "Ball")
        {
            GameManager.instance.showButton();
            Death();
        }


    }

    public void Death()
    {
        this.enabled = false;
        _coll.enabled = false;
        CameraFollow.instance.target = null;
        StartCoroutine(DestroyAfterDeath());
    }

    IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
