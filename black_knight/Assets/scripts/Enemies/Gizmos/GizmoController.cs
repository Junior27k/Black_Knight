using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoController : MonoBehaviour
{
    public float speed = 2;
    public Transform player;
    private Animator anim;
    private float sideSign;
    private string side;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            return; // Não se move enquanto ataca
        }

        // Determina a direção do player
        sideSign = Mathf.Sign(transform.position.x - player.position.x);

        if (Mathf.Abs(sideSign) == 1.0f)
        {
            side = sideSign == 1.0f ? "right" : "left";
        }

        // Rotaciona o Gizmo para frente do player
        switch (side)
        {
            case "right":
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
                break;

            case "left":
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                break;
        }

        // Move-se em direção ao player
        if (Vector2.Distance(transform.position, player.position) > 0.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), speed * Time.deltaTime);
        }
    }

}