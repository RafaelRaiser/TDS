using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyPad : MonoBehaviour, IInteragivel
{
    // Interface: Define o texto exibido para interação com o keypad
    public string TextInteragivel { get => text; set => text = value; }
    private string text;

    // Referências
    public GameObject keypadUI; // Interface do Keypad
    public TextMeshProUGUI displayText; // Exibe o código digitado no Keypad

    // Estado do puzzle e código
    private bool isNearKeypad = false;
    private bool isPuzzleActive = false; // Usado para verificar se o puzzle está ativo
    public string correctCode = "5682"; // Código correto a ser inserido
    private string playerInput = ""; // Armazena o input do jogador

    // Referência para controle da porta
    private Collider2D colidder;
    [SerializeField] private Porta script;

    private void Start()
    {
        // Define o texto inicial de interação
        DefinirTexto();
    }

    // Método chamado quando o jogador interage com o Keypad
    public void Interact()
    {
        if (!isPuzzleActive) // Verifica se o puzzle não está ativo
        {
            isPuzzleActive = true; // Ativa o estado do puzzle
            keypadUI.SetActive(true); // Mostra a UI do Keypad
            Cursor.lockState = CursorLockMode.Confined; // Libera o cursor
            Cursor.visible = true; // Torna o cursor visível
        }
    }

    // Adiciona dígitos ao código do jogador
    public void AddDigit(string digit)
    {
        playerInput += digit;
        AtualizarDisplay(); // Atualiza o display do Keypad

        // Verifica se o código inserido tem 4 dígitos
        if (playerInput.Length == 4)
        {
            CheckCode(); // Verifica se o código está correto
        }
    }

    // Reseta o código inserido pelo jogador
    public void ResetCode()
    {
        playerInput = "";
        AtualizarDisplay(); // Reseta o display
    }

    // Verifica se o código inserido está correto
    void CheckCode()
    {
        if (playerInput == correctCode)
        {
            Debug.Log("Code is correct! Door opened.");

            // Abre a porta se o código estiver correto
            if (script != null)
            {
                script.AbrirPorta();
                CloseKeypad();
            }
        }
        else
        {
            Debug.Log("Incorrect code!");
        }

        ResetCode(); // Reseta o código após a tentativa
    }

    // Define o texto exibido ao interagir com o Keypad
    public void DefinirTexto()
    {
        text = "[E] KEYPAD";
    }

    // Fecha a interface do Keypad
    public void CloseKeypad()
    {
        if (isPuzzleActive) // Verifica se o puzzle está ativo antes de fechá-lo
        {
            isPuzzleActive = false; // Desativa o estado do puzzle
            keypadUI.SetActive(false); // Esconde a UI do Keypad
            Cursor.lockState = CursorLockMode.Locked; // Bloqueia o cursor novamente
            Cursor.visible = false; // Esconde o cursor
        }
    }

    // Atualiza o display do Keypad com o input atual do jogador
    void AtualizarDisplay()
    {
        displayText.text = playerInput;
    }
}
