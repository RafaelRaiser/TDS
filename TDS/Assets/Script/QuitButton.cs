using UnityEngine;

public class QuitButton : MonoBehaviour
{
    // M�todo que ser� chamado ao clicar no bot�o
    public void QuitGame()
    {
        // Fecha o jogo
        Application.Quit();

        // Durante a execu��o no editor, esta linha garante que o jogo pare
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
