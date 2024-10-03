using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    private void Awake()
    {
        Instance = this;
    }

    public float moveSpeed = 5f;
    public float lookSensitivity = 2f;
    public float slowSpeed = 2f;
    public float gravity = -9.81f;
    CanvasActivator canvasActivator;

    public float minVerticalAngle = -90f;
    public float maxVerticalAngle = 90f;

    private Vector3 velocity;
    private float rotationX = 0f;
    private float rotationY = 0f;

    private CharacterController characterController;
    private Transform cameraTransform;

    private bool movimentar = true  ;

    public bool Movimentar { get => movimentar; set => movimentar = value; }

    void Start()
    {   
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (Movimentar)
        {
            // Movimenta��o
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? slowSpeed : moveSpeed;
            float moveForward = Input.GetAxis("Vertical") * currentSpeed;
            float moveSide = Input.GetAxis("Horizontal") * currentSpeed;

            Vector3 move = transform.right * moveSide + transform.forward * moveForward;
            characterController.Move(move * Time.deltaTime);

            // Exibe/Esconde o �cone de barulho
            if (canvasActivator != null)
            {
                if (move != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
                {
                    // Mostra o �cone apenas se o jogador estiver se movendo sem o Shift
                    canvasActivator.ShowCanvas();
                }
                else
                {
                    // Esconde o �cone se o jogador estiver parado ou movendo-se com Shift
                    canvasActivator.HideCanvas();
                }
            }

            // Controle de Vis�o (C�mera)
            float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

            rotationY += mouseX;  // Rota��o horizontal
            rotationX -= mouseY;  // Rota��o vertical

            // Limitar a rota��o vertical
            rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

            // Aplicar a rota��o ao jogador (apenas no eixo Y para evitar giro em torno de seu pr�prio eixo)
            transform.localRotation = Quaternion.Euler(0f, rotationY, 0f);

            // Aplicar rota��o � c�mera (controle de vis�o vertical)
            cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

            // Gravidade
            if (characterController.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f; // Valor pequeno para manter o jogador preso ao ch�o
            }
            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }

    }
}
