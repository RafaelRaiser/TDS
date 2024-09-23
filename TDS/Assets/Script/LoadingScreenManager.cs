
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreenManager : MonoBehaviour
{
    public GameObject loadingCanvas; // Refer�ncia ao Canvas da tela de carregamento

    public  void Start()
    {
        // Inicialmente, o Canvas de carregamento deve estar ativo
        loadingCanvas.SetActive(true);
        // Come�a a carregar a cena em background
        StartCoroutine(LoadYourScene());
    }

    IEnumerator LoadYourScene()
    {
        // Substitua "YourSceneName" pelo nome da cena que voc� deseja carregar
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Telascarregamento");

        // Enquanto a cena est� sendo carregada, mantenha a tela de carregamento ativa
        while (!asyncLoad.isDone)
        {
            // Voc� pode atualizar um progresso ou anima��o de carregamento aqui se desejar
            yield return null;
        }

        // Depois que a cena estiver carregada, desative o Canvas de carregamento
        loadingCanvas.SetActive(false);
    }
}
