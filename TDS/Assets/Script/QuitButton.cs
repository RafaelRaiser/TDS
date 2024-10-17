using UnityEngine;

public class QuitButton : MonoBehaviour
{
    // Método que será chamado ao clicar no botão
    public void QuitGame()
    {
        // Fecha o jogo
        Application.Quit();

        // Durante a execução no editor, esta linha garante que o jogo pare
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
