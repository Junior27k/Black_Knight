using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private float moveX;

    public float speed;
    public int addJumps;
    public bool isGrounded;
    public float JumpForce = 7;
    public int life;
    public TextMeshProUGUI textLife;

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

        textLife.text = life.ToString();

    }

    void FixedUpdate()
    {
        Move();
        Attack();
    }


    void Move()
    {
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        if (moveX > 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            anim.SetBool("IsRun", true);
        }
        else if (moveX < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            anim.SetBool("IsRun", true);
        }
        else
        {
            anim.SetBool("IsRun", false);
        }
    }

    void Jump()
    {

        rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        addJumps--;
    }

    void Attack(){
        if(Input.GetButtonDown("Fire1")){
            anim.Play("Attack", -1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            addJumps = 2;
            anim.SetBool("IsJump", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
            anim.SetBool("IsJump", true);
        }
    }
}
