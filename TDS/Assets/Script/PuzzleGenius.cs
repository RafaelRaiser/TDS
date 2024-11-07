using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleGenius : MonoBehaviour, IInteragivel
{
    public string TextInteragivel { get => text; set => text = value; }
    private string text;

    public GameObject geniusUI; // Interface do puzzle
    private bool isPuzzleActive = false; // Para verificar se o puzzle está ativo

    public Button[] botoesCores; // Botões das cores (associar no Inspector)
    private List<int> sequenciaCores = new List<int>();
    private List<int> inputJogador = new List<int>();
    private bool jogadorPodeJogar = false;

    private Color corOriginal; // Para armazenar a cor original do botão

    private void Start()
    {
        Interact();
    }
    // Implementando a interface IInteragivel
    public void DefinirTexto()
    {
        text = "[E] KEYPAD";
    }

    public void Interact()
    {
        if (!isPuzzleActive)
        {
            isPuzzleActive = true; // Ativa o estado do puzzle
            geniusUI.SetActive(true); // Exibe a UI do Keypad
            Cursor.lockState = CursorLockMode.Confined; // Libera o cursor
            Cursor.visible = true; // Torna o cursor visível
            ConfigurarBotoes(); // Configura os botões para chamar o método adequado
            GerarNovaCor();
            StartCoroutine(MostrarSequencia());
        }
    }

    void GerarNovaCor()
    {
        int novaCor = Random.Range(0, botoesCores.Length);
        sequenciaCores.Add(novaCor);
    }

    IEnumerator MostrarSequencia()
    {
        jogadorPodeJogar = false;
        foreach (int cor in sequenciaCores)
        {
            // Destacar a cor atual
            DestacarCor(cor);
            yield return new WaitForSeconds(1f); // Espera 1 segundo
            RestaurarCor(cor); // Restaura a cor original
            yield return new WaitForSeconds(0.5f); // Espera meio segundo antes de mostrar a próxima cor
        }
        jogadorPodeJogar = true;
    }

    void DestacarCor(int indiceCor)
    {
        // Salva a cor original do botão
        corOriginal = botoesCores[indiceCor].GetComponent<Image>().color;

        // Altera a cor do botão para um destaque
        botoesCores[indiceCor].GetComponent<Image>().color = Color.white; // Cor de destaque (branco)
    }

    void RestaurarCor(int indiceCor)
    {
        // Restaura a cor original do botão
        botoesCores[indiceCor].GetComponent<Image>().color = corOriginal;
    }

    // Função chamada quando o jogador clica em um botão de cor específico
    public void CorSelecionada(int indiceCor)
    {
        if (!jogadorPodeJogar) return;

        inputJogador.Add(indiceCor); // Adiciona a cor escolhida à lista de entrada do jogador

        // Verifica se a última cor escolhida está correta
        if (inputJogador[inputJogador.Count - 1] != sequenciaCores[inputJogador.Count - 1])
        {
            // Jogador errou, reiniciar o puzzle
            Debug.Log("Errou! Tente novamente.");
            inputJogador.Clear();
            StartCoroutine(MostrarSequencia());
        }
        else if (inputJogador.Count == sequenciaCores.Count)
        {
            // Jogador acertou a sequência, adicionar nova cor
            Debug.Log("Acertou! Nova cor adicionada.");
            inputJogador.Clear();
            GerarNovaCor();
            StartCoroutine(MostrarSequencia());
        }
    }

    // Método para configurar os botões de cores
    public void ConfigurarBotoes()
    {
        for (int i = 0; i < botoesCores.Length; i++)
        {
            int index = i; // Criando uma cópia local do índice

            // Adiciona um ouvinte ao botão para chamar a função com o índice da cor
            botoesCores[i].onClick.AddListener(() => CorSelecionada(index));
        }
    }

}
