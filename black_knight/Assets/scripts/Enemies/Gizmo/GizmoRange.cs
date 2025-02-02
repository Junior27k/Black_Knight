using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoRange : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GetComponentInParent<Animator>().Play("Attack", -1);
        }

    }
}