using UnityEngine;

public class GizmoKitten : MonoBehaviour
{
    public Transform gizmo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gizmo.GetComponent<GizmoController>().StartChasing();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gizmo.GetComponent<GizmoController>().StopChasing();
        }
    }
}
