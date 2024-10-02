using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] grassSteps; // Sons para grama
    public AudioClip[] woodSteps;  // Sons para madeira

    public CharacterController characterController;
    private string currentSurface = "Grass"; // Superf�cie atual

    void Start()
    {
        // Verifica se o AudioSource est� atribu�do corretamente
        if (!audioSource)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("Audio Source n�o encontrado! Adicione um Audio Source ao Player.");
            }
        }

        // Verifica se o CharacterController est� atribu�do corretamente
        if (!characterController)
        {
            characterController = GetComponent<CharacterController>();
            if (characterController == null)
            {
                Debug.LogError("Character Controller n�o encontrado! Adicione um Character Controller ao Player.");
            }
        }
    }

    void Update()
    {
        // Reproduz o som quando o personagem est� no ch�o e se movendo
        if (characterController.isGrounded && characterController.velocity.magnitude > 0.1f && !audioSource.isPlaying)
        {
            PlayFootstepSound();
        }
    }

    private void PlayFootstepSound()
    {
        AudioClip[] clipsToPlay = grassSteps; // Defini��o padr�o para grama

        // Verifica qual a superf�cie atual para escolher os sons corretos
        if (currentSurface == "Grass")
        {
            clipsToPlay = grassSteps;
        }
        else if (currentSurface == "Wood")
        {
            clipsToPlay = woodSteps;
        }

        // Verifica se o jogador est� andando devagar (com Shift)
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            audioSource.volume = 0.3f;  // Volume mais baixo para simular passos mais leves
            audioSource.pitch = 0.75f;  // Passos mais lentos
        }
        else
        {
            audioSource.volume = 1f;   // Volume normal ao andar
            audioSource.pitch = 1f;    // Velocidade normal dos passos
        }

        // Seleciona um som aleat�rio da superf�cie atual
        audioSource.clip = clipsToPlay[Random.Range(0, clipsToPlay.Length)];
        audioSource.Play();
    }

    // Detecta a superf�cie com base nas tags
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Grass"))
        {
            currentSurface = "Grass";
        }
        else if (hit.collider.CompareTag("Wood"))
        {
            currentSurface = "Wood";
        }
    }
}
