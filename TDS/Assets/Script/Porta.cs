using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour, IInteragivel
{
    string text;
    public string TextInteragivel { get => text; set => text = value; }

    [SerializeField] string nomePorta;

    private bool portaAberta = false;

    private Animation portaAnimacao;

    [SerializeField] bool havePuzzle;

    private void Start()
    {
        portaAnimacao = GetComponentInParent<Animation>();
        DefinirTexto();
    }

    public string NomeAnimation()
    {
        return nomePorta;
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

    public void Interact()
    {
        if (!havePuzzle)
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

            DefinirTexto();
        }
        else
        {
            text = "";
        }
        
    }

    public void DefinirTexto()
    {
        if (portaAberta)
        {
            text = "[E] Fechar";
        }
        else
        {
            text = "[E] Abrir";
        }
    }
}
