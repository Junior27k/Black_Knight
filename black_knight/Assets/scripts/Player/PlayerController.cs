using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveX;

    public float speed;
    public int addJumps;
    public bool isGrounded;
    public float JumpForce = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // moveX = Input.GetAxisRaw("Horizontal");
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");

    }

    void FixedUpdate()
    {
        move();

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                addJumps = 2;
            }
            Jump();
        }

    }


    void move()
    {
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
    }

    void Jump()
    {
        if (addJumps > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            addJumps--;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
         if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
