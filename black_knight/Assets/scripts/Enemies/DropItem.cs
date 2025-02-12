using UnityEngine;

public class DropItem : MonoBehaviour
{
    public GameObject itemPrefab;  // Prefab do item a ser dropado
    public float dropRadius = 2f;  // Raio onde o item pode cair

    void Start()
    {
        // Certifique-se de que o itemPrefab está atribuído no Inspector
        if (itemPrefab == null)
        {
            Debug.LogError("ItemPrefab não atribuído!");
        }
    }

    public void Drop()
    {
        // Instancia o item em uma posição aleatória ao redor do inimigo
        Vector3 dropPosition = transform.position + new Vector3(Random.Range(-dropRadius, dropRadius), 0, 0);
        Instantiate(itemPrefab, dropPosition, Quaternion.identity);
        Debug.Log("Item Dropped at: " + dropPosition);
    }
}
