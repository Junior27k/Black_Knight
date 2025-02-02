using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoController : MonoBehaviour
{
    // Start is called before the first frame update
    private CapsuleCollider2D colliderGizmo;
    private Animator anim;
    private float sideSign;
    private string side;

    public int life;
    public float speed;
    public Transform player;
    public GameObject range;
    void Start()
    {
        colliderGizmo = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(life <= 0){
            Die();

        }

        void Die(){
        this.enabled = false;
        colliderGizmo.enabled = false;
        range.SetActive(false);
        anim.Play("Die", -1);
        }

        if(anim.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            return;
        }

        sideSign = Mathf.Sign(transform.position.x - player.position.x);

        if (Mathf.Abs(sideSign) == 1.0f)
        {
            side = sideSign == 1.0f ? "right" : "left";
        }

        switch (side)
        {
            case "right":
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
                break;

            case "left":
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                break;  
        }

        if(Vector2.Distance(transform.position, player.position) > 0.5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.position.x, transform.position.y), speed * Time.deltaTime);
        }
    }
}