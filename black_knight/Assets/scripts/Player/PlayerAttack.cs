using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Keeper"))
        {
            collision.GetComponent<KeeperController>().life--;
        }
        if(collision.CompareTag("Gizmo"))
        {
            collision.GetComponent<GizmoController>().life--;
        }
    }
}
 