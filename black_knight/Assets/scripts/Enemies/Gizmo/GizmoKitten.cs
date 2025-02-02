@ -1,18 +1,25 @@
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoKitten : MonoBehaviour
{

    public Transform gizmo;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player"))
        {
            gizmo.GetComponent<GizmoController>().enabled= true;
            gizmo.GetComponent<Animator>().SetBool("IsRun", true);
        }
    }

    // Update is called once per frame
    void Update()
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if(collision.CompareTag("Player"))
        {
            gizmo.GetComponent<GizmoController>().enabled= false;
            gizmo.GetComponent<Animator>().SetBool("IsRun", false);
        }
    }
}