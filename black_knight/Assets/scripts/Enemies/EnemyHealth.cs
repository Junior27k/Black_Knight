using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int life, maxHealth = 10;
    private CapsuleCollider2D collider;
    private Animator anim;
    public GameObject range;

    void Start()
    {
        life = maxHealth;
        collider = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (life <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        this.enabled = false;
        collider.enabled = false;
        range.SetActive(false);
        anim.Play("Die", -1);
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            life = 0;
        }
    }
}