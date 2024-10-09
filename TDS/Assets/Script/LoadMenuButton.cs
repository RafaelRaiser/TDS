using UnityEngine;
using UnityEngine.SceneManagement; // Necess�rio para carregar cenas

public class MenuLoader : MonoBehaviour
{
    // Nome da cena de menu
    public string menuSceneName = "Menu"; // Substitua "Menu" pelo nome exato da sua cena

    // M�todo chamado uma vez por frame
    void Update()
    {
        // Verifica se a tecla Esc foi pressionada
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Carrega a cena do menu
            LoadMenu();
        }
    }

    // M�todo para carregar a cena do menu e ativar o cursor do mouse
    void LoadMenu()
    {
        // Ativa o cursor do mouse novamente
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // Desbloqueia o cursor

        // Carrega a cena do menu
        SceneManager.LoadScene(menuSceneName);
    }
}
