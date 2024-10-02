using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float slowSpeed = 2f;
    public AudioSource audioSource; // Refer�ncia ao AudioSource no player
    public List<AudioClip> grassSteps; // Sons de passos na grama
    public List<AudioClip> woodSteps; // Sons de passos na madeira
    public float stepRate = 0.5f; // Intervalo entre os sons de passos
    private float nextStep = 0f; // Controle de tempo para o pr�ximo passo
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        nextStep = stepRate; // Inicializa o tempo do pr�ximo passo
    }

    void Update()
    {
        // Movimenta��o do jogador
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? slowSpeed : moveSpeed;
        float moveForward = Input.GetAxis("Vertical") * currentSpeed;
        float moveSide = Input.GetAxis("Horizontal") * currentSpeed;

        Vector3 move = transform.right * moveSide + transform.forward * moveForward;
        characterController.Move(move * Time.deltaTime);

        // Detectar se o jogador est� andando e se est� no ch�o
        if (characterController.isGrounded && move.magnitude > 0)
        {
            PlayFootstep();
        }
    }

    void PlayFootstep()
    {
        // Verifica se j� passou o tempo para tocar o pr�ximo passo
        if (Time.time >= nextStep)
        {
            // Definir o pr�ximo tempo para tocar um passo
            nextStep = Time.time + stepRate;

            // Verifica a tag do solo para determinar o tipo de som
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
            {
                if (hit.collider.CompareTag("Grass"))
                {
                    // Randomiza e toca um som de passo na grama
                    PlayRandomClip(grassSteps);
                }
                else if (hit.collider.CompareTag("Wood"))
                {
                    // Randomiza e toca um som de passo na madeira
                    PlayRandomClip(woodSteps);
                }
            }
        }
    }

    void PlayRandomClip(List<AudioClip> clips)
    {
        if (clips.Count > 0)
        {
            AudioClip clip = clips[Random.Range(0, clips.Count)];
            audioSource.PlayOneShot(clip);
        }
    }
}