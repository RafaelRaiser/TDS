using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    // M�todo que ser� chamado quando o bot�o for clicado
    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
