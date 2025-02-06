using System.Collections;
using UnityEngine;

public class GizmoController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;
    public bool returnsToInitialPos = true; // Voltar para posição inicial quando o player sair?
    public float returnDelay = 2f; // Tempo antes de voltar
    private Vector3 initialPosition; // Posição inicial do Gizmo

    [Header("Attack Settings")]
    public int attackDamage = 1;
    public float attackCooldown = 1.5f;
    public AudioClip attackSound; // Som do ataque

    [Header("Range Settings")]
    public Transform rangeObject; // OBRIGATÓRIO: Define o range de ataque

    private Transform player;
    private Animator anim;
    private bool isAttacking = false;
    private bool playerInRange = false;
    private bool isChasing = false;
    private AudioSource audioSource;


    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initialPosition = transform.position; // Salva a posição inicial
    }

    void Update()
    {
        if (isAttacking) return; // Evita movimento durante o ataque

        if (playerInRange)
        {
            StartCoroutine(Attack());
        }
        else if (isChasing)
        {
            ChasePlayer();
        }
    }

    // 🔥 Ativado pelo GizmoKitten quando o player entra na zona
    public void StartChasing()
    {
        isChasing = true;
        anim.SetBool("IsRun", true);
    }

    // 🔥 Chamado quando o player sai da zona
    public void StopChasing()
    {
        isChasing = false;
        anim.SetBool("IsRun", false);

        if ( returnsToInitialPos)
        {
            StartCoroutine(ReturnToStart());
        }
    }

    private void ChasePlayer()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        // Vira para o player
        transform.eulerAngles = new Vector3(0f, player.position.x > transform.position.x ? 0f : -180f, 0f);

        // Move apenas se o player estiver fora do range
        if (distance > 0.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), speed * Time.deltaTime);
        }
    }

    private IEnumerator Attack()
    {
        if (isAttacking) yield break;

        isAttacking = true;
        anim.Play("Attack",-1); // Inicia a animação de ataque

        if (attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
        }

        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
    }

    // 🔥 Chamado pelo Animation Event no frame exato do golpe
    public void DealDamage()
    {
        if (playerInRange)
        {
            player.GetComponent<PlayerController>().TakeDamage(attackDamage);
        }
    }

    private IEnumerator ReturnToStart()
    {
        yield return new WaitForSeconds(returnDelay); // Espera o tempo antes de voltar

        // Determina a direção para virar corretamente
        float dir = initialPosition.x < transform.position.x ? -1 : 1;
        transform.localScale = new Vector3(dir, 1, 1);

        while (Vector2.Distance(transform.position, initialPosition) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, initialPosition, speed * Time.deltaTime);
            anim.SetBool("IsRun", true);
            yield return null;
        }

        transform.position = initialPosition;
        anim.SetBool("IsRun", false);
    }

    // 🔥 Chamado pelo Range Trigger
    public void SetPlayerInRange(bool isInRange)
    {
        playerInRange = isInRange;
    }


}
