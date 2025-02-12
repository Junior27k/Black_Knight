using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private bool isDead = false;
    

    private Animator animator;
    private DropItem dropItem;

    public Slider healthBar; // Referência ao Slider da UI
    public MonoBehaviour[] scriptsToDisable; // Lista de scripts para desativar na morte

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        DropItem dropItem = GetComponent<DropItem>();
        if (healthBar != null)
        {
            healthBar.maxValue = 1; // Normaliza para percentual
            healthBar.value = 1; // Começa com 100%
        }
    }

    public void TakeDamage(int damage = 1)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log(gameObject.name + " recebeu " + damage + " de dano. Vida restante: " + currentHealth);

        // Atualiza a barra de vida como percentual
        if (healthBar != null)
            healthBar.value = (float)currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    private void Die()
    {
        if (isDead) return;

        // Desativa todos os scripts definidos
        foreach (MonoBehaviour script in scriptsToDisable)
        {
            script.enabled = false;
        }

        isDead = true;
        animator.SetTrigger("Die");

        // Chama o método de drop
        
        if (dropItem != null)
        {
            dropItem.Drop();  // Dropa o item
        }

        

        // Desativa colisões e físicas
        GetComponent<Collider2D>().enabled = false;
        // GetComponent<Rigidbody2D>().simulated = false;

        this.enabled = false; // Desativa este script

        // Opcional: destruir o objeto após a animação de morte
        Destroy(gameObject, 10f);
    }
}
