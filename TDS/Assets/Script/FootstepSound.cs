using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] grassSteps; // Sons para grama
    public AudioClip[] woodSteps;  // Sons para madeira

    public CharacterController characterController;
    private string currentSurface = "Grass"; // Superfície atual

    void Start()
    {
        // Verifica se o AudioSource está atribuído corretamente
        if (!audioSource)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("Audio Source não encontrado! Adicione um Audio Source ao Player.");
            }
        }

        // Verifica se o CharacterController está atribuído corretamente
        if (!characterController)
        {
            characterController = GetComponent<CharacterController>();
            if (characterController == null)
            {
                Debug.LogError("Character Controller não encontrado! Adicione um Character Controller ao Player.");
            }
        }
    }

    void Update()
    {
        // Reproduz o som quando o personagem está no chão e se movendo
        if (characterController.isGrounded && characterController.velocity.magnitude > 0.1f && !audioSource.isPlaying)
        {
            PlayFootstepSound();
        }
    }

    private void PlayFootstepSound()
    {
        AudioClip[] clipsToPlay = grassSteps; // Definição padrão para grama

        // Verifica qual a superfície atual para escolher os sons corretos
        if (currentSurface == "Grass")
        {
            clipsToPlay = grassSteps;
        }
        else if (currentSurface == "Wood")
        {
            clipsToPlay = woodSteps;
        }

        // Verifica se o jogador está andando devagar (com Shift)
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

        // Seleciona um som aleatório da superfície atual
        audioSource.clip = clipsToPlay[Random.Range(0, clipsToPlay.Length)];
        audioSource.Play();
    }

    // Detecta a superfície com base nas tags
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
