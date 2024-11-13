using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mouseteste : MonoBehaviour
{
    public Button yourButton; // Referência ao botão na cena

    void Start()
    {
        // Certifique-se de que o botão esteja associado no Inspector
        if (yourButton != null)
        {
            yourButton.onClick.AddListener(OnButtonClick);
        }
    }

    void Update()
    {
        // Quando a tecla 'Esc' for pressionada, pausa a gameplay e habilita o mouse
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleGameplay();
        }
    }

    void ToggleGameplay()
    {
        // Verifica se o jogo está pausado ou não
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0; // Pausa o jogo
            Cursor.lockState = CursorLockMode.None; // Desbloqueia o cursor
            Cursor.visible = true; // Torna o cursor visível
        }
        else
        {
            Time.timeScale = 1; // Retorna o jogo ao normal
            Cursor.lockState = CursorLockMode.Locked; // Trava o cursor
            Cursor.visible = false; // Torna o cursor invisível
        }
    }

    void OnButtonClick()
    {
        Debug.Log("Botão clicado!");
        // Aqui você pode adicionar o que acontece quando o botão é clicado
    }
}
