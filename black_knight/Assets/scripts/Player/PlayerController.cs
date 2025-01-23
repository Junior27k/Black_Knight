using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
//using System.Numerics;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D playerCapsule;
    [SerializeField]
    private float moveX;

    [Header("Atributtes")]
    public float speed;
    public int addJumps;
    public float JumpForce = 7;
    public int life;

    [Header("Ground Detection")]
    public LayerMask groundLayer; //camada que representa o chao
    public float groundCheckDistance; //tamanho da linha do raycast
    public Vector2 groundCheckOffsetLeft; //offset para ponto de verificacao a esquerda
    public Vector2 groundCheckOffsetRight; //offset para ponto de verificacao a direita

    [Header("Bool")]
    [SerializeField] private bool isGrounded;
    [HideInInspector]public bool isPause;


    [Header("UI")]
    public TextMeshProUGUI textLife;

    [Header("GameObjects")]
    public GameObject gameOver;
    public GameObject canvasPause;

    [Header("GameObjects")]
    public string levelName;
    

    private void Awake(){
        //conferir se a cena foi carregada
        if(PlayerPrefs.GetInt("wasLoaded")== 1 ){
            life = PlayerPrefs.GetInt("Life", 0);
            Debug.Log("Game Loaded");
        }
    }

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
        textLife.text = life.ToString();

        if(life <= 0){
            Die();
        }

        //salvar o jogo
        if(Input.GetKeyDown(KeyCode.P)){
            string activeScene = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("Nivel Salvo", activeScene);
            PlayerPrefs.SetInt("Life", life);
            Debug.Log("Jogo Salvo");
        }

        if(Input.GetButtonDown("Cancel"))
        {
            PauseScreen();
        }

        if(isGrounded){
            addJumps = 1;
            if(Input.GetButtonDown("Jump")){
                Jump();
            }
        }
        else{
            if(Input.GetButtonDown("Jump") && addJumps > 0){
                addJumps--;
                Jump();
            }
        }

        Attack();

    }

    void FixedUpdate()
    {
        Move();
        
        //CheckGroundedStatus();
        

    }


    void Move()
    {
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        if (moveX > 0)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            anim.SetBool("IsRun", true);

            //groundCheckOffsetLeft.x = -Mathf.Abs(groundCheckOffsetLeft.x);
           // groundCheckOffsetRight.x = Mathf.Abs(groundCheckOffsetRight.x);
        }
        else if (moveX < 0)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            anim.SetBool("IsRun", true);

            //groundCheckOffsetLeft.x = Mathf.Abs(groundCheckOffsetLeft.x);
            //groundCheckOffsetRight.x = -Mathf.Abs(groundCheckOffsetRight.x);
        }
        if(moveX == 0)
        {
            anim.SetBool("IsRun", false);
        }
    }

    void Jump()
    {

        rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        anim.SetBool("IsJump", true);
    }

    void Attack()
    {
        if(Input.GetButtonDown("Fire1")){
        anim.Play("Attack", -1);

        }
        
    }


    void Die(){
        this.enabled = false;
        rb.velocity = new Vector2(0f,0f);
        rb.gravityScale = 1;
        // playerCapsule.enabled = false;
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

    void CheckGroundedStatus(){
        //calcula os pontos da origem
        Vector2 position = transform.position;
        Vector2 groundCheckPositionLeft = position + groundCheckOffsetLeft;
        Vector2 groundCheckPositionRight = position + groundCheckOffsetRight;

        //lanca os raycasts
        RaycastHit2D hitLeft = Physics2D.Raycast(groundCheckPositionLeft, Vector2.down, groundCheckDistance, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(groundCheckPositionRight, Vector2.down, groundCheckDistance, groundLayer);

        //Atualiza o valor de isGrounded
        isGrounded = hitLeft.collider != null || hitRight.collider != null;

        anim.SetBool("IsJump", !isGrounded);
    }

    void OnDrawGizmos(){
        //desenha as linhas dos raycasts
        Vector3 groundCheckPositionLeft = transform.position + new Vector3(groundCheckOffsetLeft.x, groundCheckOffsetLeft.y, 0f);
        Vector3 groundCheckPositionRight = transform.position + new Vector3(groundCheckOffsetRight.x, groundCheckOffsetRight.y, 0f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundCheckPositionLeft, groundCheckPositionLeft + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(groundCheckPositionRight, groundCheckPositionRight + Vector3.down * groundCheckDistance);
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
