using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    private KeeperController keeperController;  // Referência ao KeeperController
    private Transform player;  // Referência ao jogador

    void Start()
    {
        // Encontra o KeeperController, que está no pai (Keeper)
        keeperController = GetComponentInParent<KeeperController>();

        // Encontra o jogador na cena
        player = GameObject.FindWithTag("Player").transform;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que entrou no range é o jogador
        if (other.CompareTag("Player"))
        {
            // Chama o método TriggerAttack no KeeperController
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Se o jogador sair do range, pode adicionar lógica de reset ou outras ações
        if (other.CompareTag("Player"))
        {
            // Aqui você pode adicionar lógica para quando o jogador sair do range
            // Por exemplo, cancelar o ataque ou parar animação
        }
    }
}
