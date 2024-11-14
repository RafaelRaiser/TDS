using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    public static Notification instance;
    public GameObject panelNotification;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Sprite sucessoSprite, erroSprite, informacaoSprite;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Notificar("Informacao", "Investigue o local");
    }
    public void Notificar(string tipo, string mensagem)
    {
        StopAllCoroutines(); // Interrompe qualquer notificação anterior para reiniciar o timer

        if (tipo == "Sucesso")
        {
            panelNotification.SetActive(true);
            panelNotification.transform.GetChild(0).GetComponent<Image>().sprite = sucessoSprite;
            text.text = mensagem;
            StartCoroutine(DesativarNotificacao(panelNotification, 6f));
        }
        else if (tipo == "Erro")
        {
            panelNotification.SetActive(true);
            panelNotification.transform.GetChild(0).GetComponent<Image>().sprite = erroSprite;
            text.text = mensagem;
            StartCoroutine(DesativarNotificacao(panelNotification, 6f));
        }
        else if (tipo == "Informacao")
        {
            panelNotification.SetActive(true);
            panelNotification.transform.GetChild(0).GetComponent<Image>().sprite = informacaoSprite;
            text.text = mensagem;
            StartCoroutine(DesativarNotificacao(panelNotification, 6f));
        }
    }

    // Coroutine que desativa a notificação após um tempo especificado
    private IEnumerator DesativarNotificacao(GameObject notification, float delay)
    {
        yield return new WaitForSeconds(delay); // Espera o tempo especificado (10 segundos)
        notification.SetActive(false); // Desativa a notificação
    }
}


