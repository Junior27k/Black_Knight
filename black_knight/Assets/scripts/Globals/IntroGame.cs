using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class IntroGame : MonoBehaviour
{
    public Image[] imagens; // Arraste as imagens para este array no Inspector
    public float tempoTroca = 4f;
    private int imagemAtual = 0;

    void Start()
    {
        StartCoroutine(TrocarImagens());
    }

    IEnumerator TrocarImagens()
    {
        while (imagemAtual < imagens.Length)
        {
            imagens[imagemAtual].gameObject.SetActive(true);
            yield return new WaitForSeconds(tempoTroca);
            imagens[imagemAtual].gameObject.SetActive(false);
            imagemAtual++;
        }
        // Exemplo 2: Carregar próxima cena
        SceneManager.LoadScene(0);
    }
}
