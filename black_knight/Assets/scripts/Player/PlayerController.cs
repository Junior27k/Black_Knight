using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    private CapsuleCollider2D playerCollider;

    [Header("Attributes")]
    public float speed = 5f;
    public int maxJumps = 2;
    public float jumpForce = 7f;
    public int life = 3;

    [Header("Ground Detection")]
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.15f;
    public Vector2 groundCheckOffsetLeft = new Vector2(-0.5f, -0.5f);
    public Vector2 groundCheckOffsetRight = new Vector2(0.5f, -0.5f);

    [Header("GameObjects")]
    public GameObject gameOver;
    public GameObject canvasPause;

    private bool isGrounded;
    private int remainingJumps;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        remainingJumps = maxJumps;
        GameManager.Instance.UpdateLifeUI(life);
    }

    private void Update()
    {
        HandleInput();
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
            GameManager.Instance.SaveGame(life);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            GameManager.Instance.TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.X)) // Ataque normal
        {
            Attack("Attack");
            
        }
        if (Input.GetButtonDown("Jump"))
        {
            TryJump();
        }
        if (Input.GetKeyDown(KeyCode.Z)) // Ataque especial
        {
            anim.SetTrigger("Attack2");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) // Agachar
        {
            Crouch(true);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow)) // Levantar
        {
            Crouch(false);
        }
    }

    private void CheckLife()
    {
        if (life <= 0)
        {
            StartCoroutine(Die());
        }
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

    private void Crouch(bool isCrouching)
    {
        anim.SetBool("IsCrouch", isCrouching);

        if (isCrouching)
        {
            Vector2 currentOffset = playerCollider.offset;
            currentOffset.y = -0.1160694f;
            playerCollider.offset = currentOffset;
            playerCollider.size = new Vector2(0.2f,0.281542f);
            speed *= 0.5f; // Diminui a velocidade ao abaixar
        }
        else
        {
            Vector2 currentOffset = playerCollider.offset;
            currentOffset.y = -0.06201427f;
            playerCollider.offset = currentOffset;
            playerCollider.size = new Vector2(0.2f,0.3896523f);
            playerCollider.enabled = true;
            speed *= 2f; // Retorna à velocidade normal ao levantar
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
        GetComponent<SoundPlayer>().PlaySound("Jump");
    }

    private void Attack(string type = "Attack")
    {
        anim.Play(type, -1);
        GetComponent<SoundPlayer>().PlaySound(type);
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        GameManager.Instance.UpdateLifeUI(life);

        // Aciona a animação de hit
        anim.Play("Hit", -1);

        if (life <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        this.enabled = false;
        rb.velocity = Vector2.zero;
        anim.Play("Die", -1);
        yield return new WaitForSeconds(2f);
        GameManager.Instance.RespawnPlayer(this);
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

    private void OnDrawGizmos()
    {
        Vector3 groundCheckPositionLeft = transform.position + new Vector3(groundCheckOffsetLeft.x, groundCheckOffsetLeft.y, 0f);
        Vector3 groundCheckPositionRight = transform.position + new Vector3(groundCheckOffsetRight.x, groundCheckOffsetRight.y, 0f);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(groundCheckPositionLeft, groundCheckPositionLeft + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(groundCheckPositionRight, groundCheckPositionRight + Vector3.down * groundCheckDistance);
    }
}
