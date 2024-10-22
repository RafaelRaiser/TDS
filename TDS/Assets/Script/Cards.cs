using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cards : MonoBehaviour,IInteragivel

   
{
    public GameObject painel; // Painel a ser ativado/desativado
    [SerializeField] private string textoInteracao = "Pressione E para interagir"; // Texto padr�o
    public TextMeshProUGUI textoTMP; // Componente TextMeshPro opcional para exibir o texto dinamicamente

    public string TextInteragivel { get; set; }

    private void Start()
    {
        DefinirTexto(); // Define o texto no in�cio
    }

    // M�todo chamado quando o jogador pressiona "E"
    public void Interact()
    {
        // Alterna a visibilidade do painel
        painel.SetActive(!painel.activeSelf) ;
        
    }

    // M�todo para definir o texto de intera��o
    public void DefinirTexto()
    {
        // Define o texto que ser� exibido ao interagir
        TextInteragivel = textoInteracao;

        // Atualiza um TextMeshProUGUI na UI (opcional)
        if (textoTMP != null)
        {
            textoTMP.text = TextInteragivel;
        }
    }
}


