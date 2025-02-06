using UnityEngine;

public class GizmoRange : MonoBehaviour
{
    private GizmoController gizmoController;

    private void Start()
    {
        gizmoController = GetComponentInParent<GizmoController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gizmoController.SetPlayerInRange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gizmoController.SetPlayerInRange(false);
        }
    }
}
