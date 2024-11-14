using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cards : MonoBehaviour, IInteragivel
{
    public GameObject painel; // Painel independente na cena
    [SerializeField] private string textoInteracao = "Pressione E para interagir"; // Texto de interação
    public TextMeshProUGUI textoTMP; // Componente TextMeshPro opcional para exibir o texto
    public Button fecharBotao; // Botão para fechar o painel

    public string TextInteragivel { get; set; }

    private void Start()
    {
        DefinirTexto(); // Define o texto de interação no início

        // Verifica se o botão foi atribuído e adiciona o evento de clique para fechar o painel
        if (fecharBotao != null)
        {
            fecharBotao.onClick.AddListener(ClosePanel);
        }
    }

    // Método chamado quando o jogador pressiona "E"
    public void Interact()
    {
        // Ativa o painel e executa a lógica de pausa e cursor
        painel.SetActive(true);

        // Pausa o jogo e permite mover o mouse
        Time.timeScale = 0; // Congela o tempo (pausa o jogo)
        Cursor.lockState = CursorLockMode.None; // Desbloqueia o cursor
        Cursor.visible = true; // Torna o cursor visível

        // Desabilita o controle do personagem
        DisableCharacterControl();

        // O objeto `Cards` será removido da cena após abrir o painel
    }

    // Método para fechar o painel quando o botão for clicado
    private void ClosePanel()
    {
        painel.SetActive(false); // Fecha o painel
        Time.timeScale = 1; // Retoma o jogo (despausa)
        Cursor.lockState = CursorLockMode.Locked; // Trava o cursor
        Cursor.visible = false; // Torna o cursor invisível

        // Restaura o controle do personagem
        EnableCharacterControl();
    }

    // Método para definir o texto de interação
    public void DefinirTexto()
    {
        TextInteragivel = textoInteracao;

        if (textoTMP != null)
        {
            textoTMP.text = TextInteragivel;
        }
    }

    // Método para desabilitar o controle do personagem
    private void DisableCharacterControl()
    {
        // Desabilitar o componente de controle do personagem, por exemplo:
        // GetComponent<PlayerMovement>().enabled = false;
    }

    // Método para habilitar o controle do personagem
    private void EnableCharacterControl()
    {
        // Habilitar o componente de controle do personagem, por exemplo:
        // GetComponent<PlayerMovement>().enabled = true;
    }
}
