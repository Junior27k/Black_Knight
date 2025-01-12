using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animatr anim;
    private float moveX;

    public float speed;
    public int addJumps;
    public bool isGrounded;
    public float JumpForce = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && addJumps > 0)
        {
            Jump();
        }


    }

    void FixedUpdate()
    {
        move();

    }


    void move()
    {
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

    

        if (moveX > 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            anim.setBool("isRun", true);
        }
        else if (moveX < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            anim.setBool("isRun", true);
        }
        else
        {
            anim.setBool("isRun", false);

        }
    }

    void Jump()
    {

        rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        addJumps--;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            addJumps = 2;
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
