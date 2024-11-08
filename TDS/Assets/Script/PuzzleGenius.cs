using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MostrarSequencia : MonoBehaviour, IInteragivel
{

    public Image spriteSequencia;
    public List<int> sequencia = new List<int>();
    private List<int> entradaJogador = new List<int>();
    public float tempoEspera = 1.0f;
    public GameObject uiGenius;

    public Color amarelo, vermelho, azul, verde;

    public string TextInteragivel { get => text; set => text = value; }
    private string text;

    private bool isPuzzleActive = false;

    public void Interact()
    {
        GerarSequencia();
        StartCoroutine(ExibirSequencia());

        isPuzzleActive = true; // Ativa o estado do puzzle
        uiGenius.SetActive(true); // Mostra a UI do puzzle
        Cursor.lockState = CursorLockMode.None; // Desbloqueia o cursor
        Cursor.visible = true; // Torna o cursor visível
    }

    void GerarSequencia()
    {
        // Exemplo: Gerando uma sequência de 4 cores aleatórias
        for (int i = 0; i < 4; i++)
        {
            sequencia.Add(Random.Range(1, 5));
        }
    }

    IEnumerator ExibirSequencia()
    {
        foreach (int cor in sequencia)
        {
            Debug.Log("Exibindo cor: " + cor); // Verificar a sequência
            switch (cor)
            {
                case 1:
                    spriteSequencia.color = amarelo;
                    break;
                case 2:
                    spriteSequencia.color = vermelho;
                    break;
                case 3:
                    spriteSequencia.color = azul;
                    break;
                case 4:
                    spriteSequencia.color = verde;
                    break;
            }
            yield return new WaitForSeconds(tempoEspera);
            spriteSequencia.color = Color.white;
            yield return new WaitForSeconds(0.5f);
        }
    }


    public void BotaoPressionado(int cor)
    {
        entradaJogador.Add(cor);
        VerificarSequencia();
    }

    void VerificarSequencia()
    {
        for (int i = 0; i < entradaJogador.Count; i++)
        {
            if (entradaJogador[i] != sequencia[i])
            {
                // Resetar entrada e avisar o jogador
                entradaJogador.Clear();
                Debug.Log("Sequência incorreta! Tente novamente.");
                return;
            }
        }

        if (entradaJogador.Count == sequencia.Count)
        {
            Debug.Log("Sequência correta!");
            // Ação para quando o jogador acertar
        }
    }

    public void DefinirTexto()
    {
        text = "[E] GENIUS";
    }
}
