using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Importa o TextMesh Pro

public class Sanidade : MonoBehaviour
{
    public float sanidadeMaxima = 100f;
    private float sanidadeAtual;

    public Image barraSanidade; // A imagem da barra de sanidade
    public TextMeshProUGUI suprimentosText; // Mudado para TextMeshProUGUI
    public int quantidadeSuprimentos = 10;

    // Cores para o gradiente de sanidade
    public Color corInicial = Color.green; // Sanidade completa
    public Color corFinal = Color.red; // Sem sanidade (cor de "sangue")

    private float timerReducao = 2f;
    private float proximaReducao = 0f;

    void Start()
    {
        sanidadeAtual = sanidadeMaxima;
        AtualizarBarraSanidade();
        AtualizarTextoSuprimentos();
    }

    void Update()
    {
        // Reduz sanidade automaticamente a cada 2 segundos
        if (Time.time >= proximaReducao)
        {
            ReduzirSanidade(5f);
            proximaReducao = Time.time + timerReducao;
        }

        // Usa suprimentos para recuperar sanidade
        if (Input.GetKeyDown(KeyCode.H))
        {
            RecuperarSanidade();
        }
    }

    void ReduzirSanidade(float quantidade)
    {
        sanidadeAtual -= quantidade;
        sanidadeAtual = Mathf.Clamp(sanidadeAtual, 0, sanidadeMaxima);
        AtualizarBarraSanidade();
    }

    void RecuperarSanidade()
    {
        if (quantidadeSuprimentos > 0 && sanidadeAtual < sanidadeMaxima)
        {
            sanidadeAtual += sanidadeMaxima / 2;
            sanidadeAtual = Mathf.Clamp(sanidadeAtual, 0, sanidadeMaxima);
            quantidadeSuprimentos--;
            AtualizarBarraSanidade();
            AtualizarTextoSuprimentos();
        }
    }

    void AtualizarBarraSanidade()
    {
        // Define o preenchimento da barra como uma proporção da sanidade
        barraSanidade.fillAmount = sanidadeAtual / sanidadeMaxima;

        // Calcula a cor com base no valor da sanidade
        barraSanidade.color = Color.Lerp(corFinal, corInicial, sanidadeAtual / sanidadeMaxima);
    }

    void AtualizarTextoSuprimentos()
    {
        suprimentosText.text = "Suprimentos: " + quantidadeSuprimentos;
    }
}
