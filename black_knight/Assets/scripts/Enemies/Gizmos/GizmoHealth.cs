using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GizmoHealth : MonoBehaviour
{
    public int life, maxHealth = 7;
    public Slider healthSlider;
    private bool isDead = false;

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

    public bool IsDead(){
        return isDead;
    }

    void Die()
    {
        isDead = true;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<GizmoController>().enabled = false;
        GetComponent<Animator>().Play("Die", -1);
        healthSlider.enabled = false;
        this.enabled = false;
    }

    public void TakeDamage(int damage = 1){
        life -= damage;
        UpdateHealthBar();


        // Aciona a animação de hit
        // anim.SetTrigger("Hit");
    }
}