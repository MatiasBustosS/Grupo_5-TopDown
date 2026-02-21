using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class NPC : MonoBehaviour
{
    [SerializeField] private GameManagerSO gameManager;
    [SerializeField, TextArea(1, 5)] private string[] frases;
    [SerializeField] private float tiempoEntreLetras;
    [SerializeField] private GameObject marcoDialogo;
    [SerializeField] private TextMeshProUGUI textoDialogo;
    [SerializeField] private GameObject canvasAyuda;
    private int indiceActual=-1;
    private bool hablando=false;


    public void Interactuar()
    {
        gameManager.CambiarEstadoPlayer(false);
        marcoDialogo.SetActive(true);
        if (!hablando)
        {
            SiguienteFrase();
        }
        else
        {
            CompletarFrase();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canvasAyuda.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        canvasAyuda.SetActive(false);
    }

    private void CompletarFrase()
    {
        StopAllCoroutines();
        textoDialogo.text = frases[indiceActual].ToString();
        hablando = false;
    }

    private void SiguienteFrase()
    {
        indiceActual++;
        if (indiceActual>=frases.Length)
        {
            TerminarDialogo();
        }
        else
        {
            StartCoroutine(EscribirFrase());
        }
    }

    private void TerminarDialogo()
    {
        hablando = false;
        marcoDialogo.SetActive(false);
        textoDialogo.text = "";
        indiceActual = -1;
        gameManager.CambiarEstadoPlayer(true);
    }

    IEnumerator EscribirFrase()
    {
        hablando = true;
        textoDialogo.text = "";
        char[] caracteresFrase=frases[indiceActual].ToCharArray();
        foreach  (char caracter in caracteresFrase)
        {
            textoDialogo.text += caracter;
            yield return new WaitForSeconds(tiempoEntreLetras);
        }
        hablando = false;
    }
}
