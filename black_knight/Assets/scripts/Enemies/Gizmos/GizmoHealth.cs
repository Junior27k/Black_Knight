using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GizmoHealth : MonoBehaviour
{
    public int life, maxHealth = 7;
    public Slider healthSlider;

    void Start()
    {
        life = maxHealth;
        healthSlider.gameObject.SetActive(false);
    }

    void Update()
    {
        if (life <= 0)
        {
            Die();
        }

        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        float healthPercent = (float)life / maxHealth;
        healthSlider.value = healthPercent;
        if (life <= 0)
        {
            healthSlider.gameObject.SetActive(false);
        }
    }

    void Die()
    {
        this.enabled = false;
        healthSlider.enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Animator>().Play("Die", -1);
    }

    public void TakeDamage(int damage = 1){
        life -= damage;
        UpdateHealthBar();


        // Aciona a animação de hit
        // anim.SetTrigger("Hit");
    }
}