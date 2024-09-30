using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    [SerializeField] string nomePorta;
    private bool portaAberta = false;
    private Animation portaAnimacao;

    private void Start()
    {
        portaAnimacao = GetComponentInParent<Animation>();
    }

    public string NomeAnimation()
    {
        return nomePorta;
    }

    public void AlternarPorta()
    {
        portaAberta = !portaAberta; // Alterna entre abrir e fechar

        if (portaAberta)
        {
            AbrirPorta();
        }
        else
        {
            FecharPorta();
        }
    }

    public bool EstaAberta()
    {
        return portaAberta;
    }

    public void AbrirPorta()
    {
        if (portaAnimacao != null)
        {
            portaAnimacao.Play(nomePorta + "_Open"); // Toca animação de abrir
        }
    }

    private void FecharPorta()
    {
        if (portaAnimacao != null)
        {
            portaAnimacao.Play(nomePorta + "_Close"); // Toca animação de fechar
        }
    }
}
