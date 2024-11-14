using System.Collections;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
    public static Notification instance;
    public GameObject sucessoNotification;
    public GameObject erroNotification;
    public GameObject informacaoNotification;

    private void Awake()
    {
        instance = this;
    }

    public void Notificar(string tipo, string mensagem)
    {
        StopAllCoroutines(); // Interrompe qualquer notificação anterior para reiniciar o timer

        if (tipo == "Sucesso")
        {
            sucessoNotification.SetActive(true);
            sucessoNotification.GetComponent<TextMeshProUGUI>().text = mensagem;
            StartCoroutine(DesativarNotificacao(sucessoNotification, 10f));
        }
        else if (tipo == "Erro")
        {
            erroNotification.SetActive(true);
            erroNotification.GetComponent<TextMeshProUGUI>().text = mensagem;
            StartCoroutine(DesativarNotificacao(erroNotification, 10f));
        }
        else if (tipo == "Informacao")
        {
            informacaoNotification.SetActive(true);
            informacaoNotification.GetComponent<TextMeshProUGUI>().text = mensagem;
            StartCoroutine(DesativarNotificacao(informacaoNotification, 10f));
        }
    }

    // Coroutine que desativa a notificação após um tempo especificado
    private IEnumerator DesativarNotificacao(GameObject notification, float delay)
    {
        yield return new WaitForSeconds(delay); // Espera o tempo especificado (10 segundos)
        notification.SetActive(false); // Desativa a notificação
    }
}


