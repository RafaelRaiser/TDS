using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardsManager : MonoBehaviour
{
    public GameObject painel; // Painel a ser ativado/desativado

    private void Update()
    {
        // Verifica se o botão foi atribuído e adiciona o evento de clique
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePanel();
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
