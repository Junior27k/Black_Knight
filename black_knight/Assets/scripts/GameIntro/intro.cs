using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class intro : MonoBehaviour
{
    public Image displayImage; // A Image no Canvas onde as imagens serão trocadas
    public Sprite[] introImages; // Array com todas as imagens da introdução
    public float delayBetweenImages = 3f; // Tempo entre cada troca automática
    private int currentIndex = 0; // Índice da imagem atual

    void Start()
    {
        if (introImages.Length > 0)
        {
            displayImage.sprite = introImages[0]; // Define a primeira imagem
            StartCoroutine(AutoNextImage()); // Inicia a troca automática
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Avança ao pressionar "Espaço"
        {
            NextImage();
        }
    }

    IEnumerator AutoNextImage()
    {
        while (currentIndex < introImages.Length - 1)
        {
            yield return new WaitForSeconds(delayBetweenImages);
            NextImage();
        }
    }

    public void NextImage()
    {
        if (currentIndex < introImages.Length - 1)
        {
            currentIndex++;
            displayImage.sprite = introImages[currentIndex];
        }
        else
        {
            EndIntro(); // Finaliza a introdução
        }
    }

    void EndIntro()
    {
        gameObject.SetActive(false); // Desativa o Canvas ou este GameObject
        Debug.Log("Introdução finalizada.");
    }
}
