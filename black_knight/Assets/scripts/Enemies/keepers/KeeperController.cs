using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeperController : MonoBehaviour
{
    private CapsuleCollider2D colliderKeeper;
    private Animator anim;
    private bool goRight;

    public int life;
    public float speed;

    public Transform a;
    public Transform b;

    // Start is called before the first frame update
    void Start()
    {
        colliderKeeper = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            return;
        }

        goRight = Vector2.Distance(transform.position, a.position) < 0.1f;

        if (goRight)
        {   
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            transform.position = Vector2.MoveTowards(transform.position, b.position, speed * Time.deltaTime);

        }
        else
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            transform.position = Vector2.MoveTowards(transform.position, a.position, speed * Time.deltaTime);

        }

    }
}
