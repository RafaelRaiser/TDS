using UnityEngine;
using UnityEngine.UI;

public class PuzzleLigarFios : MonoBehaviour, IInteragivel
{
    public GameObject[] fiosEsquerda; // Fios do lado esquerdo (Image)
    public GameObject[] fiosDireita;  // Fios do lado direito (Image)
    public LineRenderer[] linhas;     // LineRenderer para desenhar as linhas entre os fios
    public GameObject canvasPuzzle;   // O canvas onde o puzzle � mostrado

    public string TextInteragivel { get => text; set => text = value; }
    private string text;

    private GameObject fioSelecionado = null;  // Armazena o fio selecionado
    private bool puzzleAtivo = false;          // Verifica se o puzzle est� ativo

    // M�todo chamado ao interagir com o objeto
    public void Interact()
    {
        if (!puzzleAtivo)
        {
            // Ativa o canvas do puzzle
            canvasPuzzle.SetActive(true);
            puzzleAtivo = true;
        }
        else
        {
            // Desativa o canvas do puzzle se o jogador sair
            canvasPuzzle.SetActive(false);
            puzzleAtivo = false;
        }
    }

    // M�todo chamado para selecionar um fio ao clicar em uma Image
    public void SelecionarFio(Image fio)
    {
        if (fioSelecionado == null)
        {
            // Seleciona o primeiro fio
            fioSelecionado = fio.gameObject;
        }
        else
        {
            // Tenta conectar com o segundo fio
            if (VerificarLigacao(fioSelecionado, fio.gameObject))
            {
                ConectarFios(fioSelecionado, fio.gameObject);
                fioSelecionado = null;  // Reset para nova sele��o
            }
            else
            {
                Debug.Log("Fios incorretos.");
                fioSelecionado = null;  // Reset se for incorreto
            }
        }
    }

    // Verifica se os fios s�o da mesma cor (ou outra propriedade)
    bool VerificarLigacao(GameObject fioEsquerdo, GameObject fioDireito)
    {
        return fioEsquerdo.name == fioDireito.name;  // Compara��o baseada no nome
    }

    // Conecta os fios e desenha uma linha
    void ConectarFios(GameObject fioEsquerdo, GameObject fioDireito)
    {
        int index = System.Array.IndexOf(fiosEsquerda, fioEsquerdo);
        if (index != -1)
        {
            // Desenha a linha conectando os fios
            linhas[index].SetPosition(0, fioEsquerdo.transform.position);
            linhas[index].SetPosition(1, fioDireito.transform.position);
        }

        // Verifica se todas as conex�es est�o corretas
        if (VerificarTodasConexoes())
        {
            ligarLuzes();  // M�todo chamado ao resolver o puzzle
        }
    }

    // Verifica se todas as linhas est�o conectadas
    bool VerificarTodasConexoes()
    {
        foreach (LineRenderer linha in linhas)
        {
            if (linha.positionCount == 0) return false;  // Verifica se a linha foi desenhada
        }
        return true;
    }

    // M�todo chamado ao resolver o puzzle
    void ligarLuzes()
    {
        Debug.Log("Luzes ligadas! Puzzle resolvido.");
        // Aqui voc� pode adicionar qualquer l�gica adicional, como abrir portas ou avan�ar no jogo
    }
    public void DefinirTexto()
    {
        text = "[E] DISJUNTOR";
    }
}
