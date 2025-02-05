using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    private CapsuleCollider2D playerCapsule;

    [Header("Attributes")]
    public float speed = 5f;
    public int maxJumps = 2;
    public float jumpForce = 7f;
    public int life = 3;

    [Header("Ground Detection")]
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.1f;
    public Vector2 groundCheckOffsetLeft = new Vector2(-0.5f, -0.5f);
    public Vector2 groundCheckOffsetRight = new Vector2(0.5f, -0.5f);

    [Header("UI")]
    public TextMeshProUGUI textLife;

    [Header("GameObjects")]
    public GameObject gameOver;
    public GameObject canvasPause;

    [Header("Scene Management")]
    public string levelName;

    private bool isGrounded;
    private bool isPaused;
    private int remainingJumps;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("wasLoaded") == 1)
        {
            life = PlayerPrefs.GetInt("Life", life);
            Debug.Log("Game Loaded");
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        playerCapsule = GetComponent<CapsuleCollider2D>();
        remainingJumps = maxJumps;
    }

    private void Update()
    {
        HandleInput();
        UpdateUI();
        CheckLife();
    }

    private void FixedUpdate()
    {
        Move();
        CheckGroundedStatus();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveGame();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            TogglePause();
        }

        if (Input.GetButtonDown("Jump"))
        {
            TryJump();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    private void UpdateUI()
    {
        textLife.text = life.ToString();
    }

    private void CheckLife()
    {
        if (life <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private void SaveGame()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("Nivel Salvo", activeScene);
        PlayerPrefs.SetInt("Life", life);
        Debug.Log("Jogo Salvo");
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        canvasPause.SetActive(isPaused);
    }

    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        if (moveX != 0)
        {
            transform.eulerAngles = new Vector3(0f, moveX > 0 ? 0f : 180f, 0f);
            anim.SetBool("IsRun", true);
        }
        else
        {
            anim.SetBool("IsRun", false);
        }
    }

    private void TryJump()
    {
        if (isGrounded || remainingJumps > 0)
        {
            Jump();
            if (!isGrounded)
            {
                remainingJumps--;
            }
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        anim.SetBool("IsJump", true);
    }

    private void Attack()
    {
        anim.Play("Attack", -1);
    }

    private IEnumerator Die()
    {
        // Desativa o controle do player
        this.enabled = false;
        rb.velocity = new Vector2(0f, 0f);
        rb.gravityScale = 1;

        // Reproduz a animação de morte
        anim.Play("Die", -1);

        // Espera a animação de morte terminar (ajuste o tempo conforme necessário)
        yield return new WaitForSeconds(2f); // Tempo estimado da animação

        // Exibe a tela de Game Over
        gameOver.SetActive(true);

        // Pausa o jogo (opcional)
        Time.timeScale = 0;
    }

    private void CheckGroundedStatus()
    {
        Vector2 position = transform.position;
        Vector2 groundCheckPositionLeft = position + groundCheckOffsetLeft;
        Vector2 groundCheckPositionRight = position + groundCheckOffsetRight;

        RaycastHit2D hitLeft = Physics2D.Raycast(groundCheckPositionLeft, Vector2.down, groundCheckDistance, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(groundCheckPositionRight, Vector2.down, groundCheckDistance, groundLayer);

        isGrounded = hitLeft.collider != null || hitRight.collider != null;
        anim.SetBool("IsJump", !isGrounded);

        if (isGrounded)
        {
            remainingJumps = maxJumps;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("IsJump", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            anim.SetBool("IsJump", true);
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 groundCheckPositionLeft = transform.position + new Vector3(groundCheckOffsetLeft.x, groundCheckOffsetLeft.y, 0f);
        Vector3 groundCheckPositionRight = transform.position + new Vector3(groundCheckOffsetRight.x, groundCheckOffsetRight.y, 0f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundCheckPositionLeft, groundCheckPositionLeft + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(groundCheckPositionRight, groundCheckPositionRight + Vector3.down * groundCheckDistance);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        canvasPause.SetActive(false);
    }

    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    }
}