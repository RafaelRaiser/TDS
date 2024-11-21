using System.Collections; // Importa o namespace para utilizar coleções padrão, como listas e filas.
using System.Collections.Generic; // Importa funcionalidades para trabalhar com coleções genéricas.
using TMPro; // Importa o namespace para usar componentes de texto do TextMeshPro.
using UnityEngine; // Importa o namespace principal do Unity, que inclui classes e funcionalidades essenciais como GameObject, MonoBehaviour, etc.
using UnityEngine.UI; // Importa o namespace para manipular componentes de UI, como botões, imagens e sliders.

public class UiSetasManager : MonoBehaviour // Declara uma classe UIManager que herda de MonoBehaviour, permitindo que seja anexada a GameObjects no Unity.
{
    #region Instance
    public static UiSetasManager Instance; // Declara uma variável estática Instance do tipo UIManager, permitindo fácil acesso à instância do objeto.

    private void Awake() // Método Awake é chamado quando o script é carregado.
    {
        Instance = this; // Define a instância estática para o objeto atual, permitindo que outros scripts acessem este UIManager facilmente.
    }
    #endregion

    #region Variavel

    [SerializeField] Sprite[] sprites; // Declara um array de Sprites, permitindo sua configuração no editor para armazenar imagens.
    [SerializeField] Image[] imagens; // Declara um array de imagens UI, permitindo sua configuração no editor para modificar suas propriedades.
    [SerializeField] TextMeshProUGUI textoDoRelogio; // Declara variáveis para elementos de texto UI do TextMeshPro, que podem ser atribuídas no editor.
    #endregion

    public void AtualizarSetas(KeyCode[] setas) // Método público para atualizar as imagens das setas baseadas nas entradas fornecidas.
    {
        for (int i = 0; i < setas.Length; i++) // Itera sobre o array de setas.
        {
            if (i >= setas.Length) // Verifica se o índice está fora dos limites do array (condição que nunca será verdadeira neste loop).
            {
                imagens[i].sprite = sprites[0]; // Define a sprite da imagem como a primeira sprite (não será executado devido à condição acima).
            }
            else if (setas[i] == KeyCode.DownArrow) // Verifica se a seta atual é a seta para baixo.
            {
                imagens[i].sprite = sprites[1]; // Define a sprite da imagem correspondente para a seta para baixo.
            }
            else if (setas[i] == KeyCode.UpArrow) // Verifica se a seta atual é a seta para cima.
            {
                imagens[i].sprite = sprites[2]; // Define a sprite da imagem correspondente para a seta para cima.
            }
            else if (setas[i] == KeyCode.LeftArrow) // Verifica se a seta atual é a seta para a esquerda.
            {
                imagens[i].sprite = sprites[3]; // Define a sprite da imagem correspondente para a seta para a esquerda.
            }
            else if (setas[i] == KeyCode.RightArrow) // Verifica se a seta atual é a seta para a direita.
            {
                imagens[i].sprite = sprites[4]; // Define a sprite da imagem correspondente para a seta para a direita.
            }

            imagens[i].color = Color.white; // Define a cor da imagem como branca (resetando qualquer cor aplicada anteriormente).
        }
    }

    public void AtualizarSeta(int setaSelecionada, bool acertou) // Método público para atualizar a cor de uma seta específica com base no resultado (acerto ou erro).
    {
        if (acertou == true) // Verifica se a ação foi correta.
        {
            imagens[setaSelecionada].color = Color.green; // Define a cor da seta como verde se a ação foi correta.
        }
        else
        {
            imagens[setaSelecionada].color = Color.red; // Define a cor da seta como vermelha se a ação foi incorreta.
        }
    }

    public void AtualizarTextos(float relogio) // Método público para atualizar os textos de pontuação e relógio.
    {
        textoDoRelogio.text = relogio.ToString("00.00"); // Atualiza o texto do relógio, formatando o tempo em dois decimais.
    }
}
