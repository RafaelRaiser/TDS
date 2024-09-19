using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    // Método que será chamado quando o botão for clicado
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
