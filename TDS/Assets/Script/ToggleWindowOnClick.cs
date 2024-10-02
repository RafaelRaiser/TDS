using UnityEngine;

public class ToggleWindowOnClick : MonoBehaviour
{
    // Referência ao painel da janela (UI)
    public GameObject windowPanel;

    // Estado inicial da janela (ligada ou desligada)
    private bool isWindowActive = false;

    // Função que será chamada pelo OnClick do Button
    public void ToggleWindow()
    {
        isWindowActive = !isWindowActive;

        // Ativa ou desativa o painel da janela
        if (windowPanel != null)
        {
            windowPanel.SetActive(isWindowActive);
        }
    }
}
