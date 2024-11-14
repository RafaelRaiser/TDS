using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource; // Um único AudioSource para tocar os clipes
    public AudioClip somPorta;
    public AudioClip somOfegante;
    public AudioClip somMadeiraRangendo;
    public AudioClip somTrovoada;
    public AudioClip somGritos;
    public AudioClip trilhaTerror;
    public AudioClip trilhaTerror2;
    public AudioClip trilhaTerror3;
    public AudioClip suspense;
    public AudioClip swosh;

    private List<AudioClip> sonsAleatorios;
    private List<AudioClip> trilhasDeFundo;

    private void Start()
    {
        // Configura as listas de sons e trilhas de fundo
        ConfigurarListaDeSons();
        ConfigurarListaDeTrilhas();

        // Inicia a reprodução das trilhas de fundo e dos sons aleatórios
        StartCoroutine(TocarTrilhasDeFundo());
        StartCoroutine(TocarSonsAleatorios());
    }

    private void ConfigurarListaDeSons()
    {
        // Inicializa a lista de sons aleatórios com os efeitos desejados
        sonsAleatorios = new List<AudioClip> { somPorta, somOfegante, somMadeiraRangendo, somTrovoada, somGritos, swosh };
    }

    private void ConfigurarListaDeTrilhas()
    {
        // Inicializa a lista de trilhas de fundo
        trilhasDeFundo = new List<AudioClip> { trilhaTerror, trilhaTerror2, trilhaTerror3, suspense };
    }

    private IEnumerator TocarTrilhasDeFundo()
    {
        while (true)
        {
            // Escolhe uma trilha de fundo aleatória e toca
            AudioClip trilhaAtual = trilhasDeFundo[Random.Range(0, trilhasDeFundo.Count)];
            audioSource.clip = trilhaAtual;
            audioSource.loop = true;
            audioSource.Play();

            // Espera até a trilha de fundo terminar antes de tocar outra
            yield return new WaitForSeconds(trilhaAtual.length);
        }
    }

    private IEnumerator TocarSonsAleatorios()
    {
        while (true)
        {
            // Escolhe um som aleatório e o reproduz
            AudioClip somAtual = sonsAleatorios[Random.Range(0, sonsAleatorios.Count)];
            audioSource.PlayOneShot(somAtual);

            // Espera o som atual terminar e adiciona um intervalo aleatório antes do próximo
            yield return new WaitForSeconds(somAtual.length + Random.Range(3, 8));
        }
    }
}
