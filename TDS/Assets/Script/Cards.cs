using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cards : MonoBehaviour,IInteragivel

   
{
    public GameObject painel; // Painel a ser ativado/desativado
    [SerializeField] private string textoInteracao = ""; // Texto padrão
    //public TextMeshProUGUI textoTMP; // Componente TextMeshPro opcional para exibir o texto dinamicamente

    public string TextInteragivel { get; set; }

    private void Start()
    {
        DefinirTexto(); // Define o texto no início
    }
    // Método chamado quando o jogador pressiona "E"
    public void Interact()
    {
        // Alterna a visibilidade do painel
        painel.SetActive(!painel.activeSelf) ;
        
    }

    // Método para definir o texto de interação
    public void DefinirTexto()
    {
        // Define o texto que será exibido ao interagir
        TextInteragivel = textoInteracao;
    }
}


