using UnityEngine;

public class KeeperAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public int attackDamage = 1; // Dano causado pelo ataque
    public float attackCooldown = 0f; // Tempo de espera entre ataques
    public Collider2D attackCollider; // Collider2D que define a área de ataque
    public LayerMask playerLayer; // Layer do jogador

    private Animator animator;
    private bool canAttack = true;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Verifica se o Collider2D foi atribuído
        if (attackCollider == null)
        {
            Debug.LogError("Attack Collider not assigned in KeeperAttack!");
        }
    }

    // Método para iniciar o ataque
    public void PerformAttack()
    {
        if (canAttack)
        {
            // Dispara a animação de ataque
            animator.SetTrigger("Attack");

            // Inicia o cooldown do ataque
            canAttack = false;
            Invoke("ResetAttack", attackCooldown);
        }
    }

    // Método chamado pelo Animation Event
    public void OnAttackAnimationEvent()
    {
        // Aplica o dano ao jogador
        DealDamage();
    }

    // Função que aplica dano ao jogador
    private void DealDamage()
    {
        // Verifica se o Collider2D foi atribuído
        if (attackCollider == null)
        {
            Debug.LogWarning("Attack Collider not assigned!");
            return;
        }

        // Detecta colisões com o jogador dentro da área do Collider2D
        Collider2D[] hitPlayers = Physics2D.OverlapAreaAll(attackCollider.bounds.min, attackCollider.bounds.max, playerLayer);

        foreach (Collider2D playerCollider in hitPlayers)
        {
            // Verifica se o collider pertence ao jogador
            if (playerCollider.CompareTag("Player"))
            {
                // Obtém o componente PlayerController do jogador
                PlayerController player = playerCollider.GetComponent<PlayerController>();

                if (player != null)
                {
                    // Aplica dano ao jogador
                    player.TakeDamage(attackDamage);
                    Debug.Log("Dealt " + attackDamage + " damage to the player!");
                }
            }
        }
    }

    // Reseta o cooldown do ataque
    void ResetAttack()
    {
        canAttack = true;
    }
}