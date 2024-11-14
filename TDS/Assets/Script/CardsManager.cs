using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsManager : MonoBehaviour
{
    public GameObject painel; // Painel a ser ativado/desativado
    public Button fecharBotao; // Botão para fechar o painel

    private void Start()
    {
        // Verifica se o botão foi atribuído e adiciona o evento de clique
        if (fecharBotao != null)
        {
            fecharBotao.onClick.AddListener(ClosePanel);
        }
    }

    public void OpenPanel()
    {
        painel.SetActive(true);
        Time.timeScale = 0; // Pausa o jogo
        Cursor.lockState = CursorLockMode.None; // Libera o cursor
        Cursor.visible = true; // Exibe o cursor
    }

    private void ClosePanel()
    {
        painel.SetActive(false);
        Time.timeScale = 1; // Retoma o jogo
        Cursor.lockState = CursorLockMode.Locked; // Trava o cursor
        Cursor.visible = false; // Esconde o cursor
    }
}
