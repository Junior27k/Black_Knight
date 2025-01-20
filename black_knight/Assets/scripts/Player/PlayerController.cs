using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D playerCapsule;
    private float moveX;
    public string levelName;

    public float speed;
    public int addJumps;
    public bool isGrounded;
    public float JumpForce = 7;
    public int life;
    public TextMeshProUGUI textLife;

    public GameObject gameOver;
    public GameObject canvasPause;
    public bool isPause;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerCapsule = GetComponent<CapsuleCollider2D>();

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


        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }


        if(life <= 0 ){
            Die();
        }

        if(Input.GetButtonDown("Cancel"))
        {
            PauseScreen();
        }

    }

    void FixedUpdate()
    {
        Move();

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

    void Attack()
    {
        anim.Play("Attack", -1);
        
    }


    void Die(){
        this.enabled = false;
        playerCapsule.enabled = false;
        rb.gravityScale = 0;
        anim.Play("Die", -1);
        gameOver.SetActive(true);
    }

    void PauseScreen()
    {
        if(isPause)
        {
            isPause = false;
            Time.timeScale = 1;
            canvasPause.SetActive(false);
        }
        else
        {
            isPause = true;
            Time.timeScale = 0;
            canvasPause.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        isPause = false;
        Time.timeScale = 1;
        canvasPause.SetActive(false);
    }

    public void BackMenu()
    {
        SceneManager.LoadScene(0);
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
