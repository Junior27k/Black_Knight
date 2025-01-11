using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveX;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveX = Input.GetAxisRaw("Horizontal");
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");

    }

    void FixedUpdate()
    {
        move();
    }

    void move()
    {
        rb.velocity = new Vector2(moveX * speed, rb.linearVelocity.y);
    }
}
