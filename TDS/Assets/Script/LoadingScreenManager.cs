
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreenManager : MonoBehaviour
{
    public GameObject loadingCanvas; // Referência ao Canvas da tela de carregamento

    public  void Start()
    {
        // Inicialmente, o Canvas de carregamento deve estar ativo
        loadingCanvas.SetActive(true);
        // Começa a carregar a cena em background
        StartCoroutine(LoadYourScene());
    }

    IEnumerator LoadYourScene()
    {
        // Substitua "YourSceneName" pelo nome da cena que você deseja carregar
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Telascarregamento");

        // Enquanto a cena está sendo carregada, mantenha a tela de carregamento ativa
        while (!asyncLoad.isDone)
        {
            // Você pode atualizar um progresso ou animação de carregamento aqui se desejar
            yield return null;
        }

        // Depois que a cena estiver carregada, desative o Canvas de carregamento
        loadingCanvas.SetActive(false);
    }
}
