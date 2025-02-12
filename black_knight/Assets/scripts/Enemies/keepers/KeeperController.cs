using UnityEngine;

public class KeeperController : MonoBehaviour
{
    [Header("Patrol Settings")]
    public Transform pointB;
    public Transform pointA;
    public float patrolSpeed = 2f;

    [Header("Attack Settings")]
    public float visionRange = 5f;
    public float attackRange = 1.5f;
    public Transform player;
    public float chaseSpeed = 3.5f;

    private bool movingToPointB = true;
    private Rigidbody2D rb;
    private Animator animator;
    private KeeperAttack keeperAttack; // Referência ao script de ataque

    void Start()
    {
        // rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        keeperAttack = GetComponent<KeeperAttack>(); // Obtém o componente KeeperAttack
    }

    void Update()
    {
        float playerDistance = Vector2.Distance(transform.position, player.position);

        if (playerDistance <= attackRange)
        {
            Attack();
            Debug.Log("Atacou o Player");
        }
        else if (playerDistance <= visionRange)
        {
            ChasePlayer();
            Debug.Log("Persseguir o Player");
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        animator.SetBool("Walking", true);

        if (movingToPointB)
        {
            transform.position = Vector2.MoveTowards(transform.position, pointB.position, patrolSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, pointB.position) < 0.2f)
            {
                Flip();
                movingToPointB = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, pointA.position, patrolSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, pointA.position) < 0.2f)
            {
                Flip();
                movingToPointB = true;
            }
        }
    }

    void ChasePlayer()
    {
        animator.SetBool("Walking", true);
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);

        if ((player.position.x > transform.position.x && transform.localScale.x < 0) ||
            (player.position.x < transform.position.x && transform.localScale.x > 0))
        {
            Flip();
        }
    }

    void Attack()
    {
        // Chama o método de ataque do KeeperAttack
        keeperAttack.PerformAttack();
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnDrawGizmosSelected()
    {
        // Cor azul para o range de caça
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        // Cor vermelha para o range de ataque
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}