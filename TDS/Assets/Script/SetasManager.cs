using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetasManager : MonoBehaviour, IInteragivel
{
    int teclaAtual; // Pontuação e índice da tecla atual.
    float relogio; // Cronômetro.
    KeyCode[] teclas; // Array de teclas.
    string text;
    public GameObject UISetas;
    public GameObject[] luzesAreaRestrita;

    public string TextInteragivel { get => text; set => text = value; }

    public void Interact()
    {
        UISetas.SetActive(true);
        GerarSetas();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChecarTeclas(KeyCode.DownArrow);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChecarTeclas(KeyCode.UpArrow);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChecarTeclas(KeyCode.LeftArrow);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChecarTeclas(KeyCode.RightArrow);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UISetas.SetActive(false);
        }

        ContagemRegressiva(); // Atualiza o cronômetro.
        DefinirTexto();
    }

    private void ContagemRegressiva()
    {
        relogio -= Time.deltaTime; // Diminui o tempo restante.
        UiSetasManager.Instance.AtualizarTextos(relogio); // Atualiza a interface.

        if (relogio <= 0) // Caso o tempo acabe.
        {
            ReiniciarJogo(); // Reinicia o jogo.
        }
    }

    private void GerarSetas()
    {
        teclaAtual = 0; // Reseta a tecla atual.
        teclas = new KeyCode[6]; // Define a sequência fixa de 6 setas.

        for (int i = 0; i < teclas.Length; i++)
        {
            teclas[i] = (KeyCode)Random.Range(273, 277); // Gera teclas aleatórias (setas).
        }

        relogio = 3f; // Define o tempo como 4 segundos.
        UiSetasManager.Instance.AtualizarSetas(teclas); // Atualiza a interface.
    }

    private void ChecarTeclas(KeyCode teclaPressionada)
    {
        if (teclaPressionada == teclas[teclaAtual]) // Se a tecla estiver correta.
        {
            UiSetasManager.Instance.AtualizarSeta(teclaAtual, true); // Atualiza a interface.
        }
        else
        {
            ReiniciarJogo(); // Reinicia o jogo ao erro.
            return; // Sai do método para evitar progresso na sequência.
        }

        teclaAtual++; // Avança para a próxima tecla.

        if (teclaAtual == teclas.Length) // Se todas as teclas forem acertadas.
        {
            UISetas.SetActive(false);
            Notification.instance.Notificar("Sucesso", "Sequência Correta");
            LigarLuzes();
        }
    }

    private void ReiniciarJogo()
    {
        GerarSetas(); // Gera uma nova sequência.
    }
    public void DefinirTexto()
    {
        text = "[E] Puzzle Eletrico";
    }

    public void LigarLuzes()
    {
        for (int i = 0; i < luzesAreaRestrita.Length; i++) // Percorre o vetor usando o índice.
        {
            luzesAreaRestrita[i].SetActive(true);
        }

    }
}
