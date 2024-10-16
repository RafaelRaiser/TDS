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

    public Vector3 move;

    public float moveSpeed = 3f;
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
            // Movimentação
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? slowSpeed : moveSpeed;
            float moveForward = Input.GetAxis("Vertical") * currentSpeed;
            float moveSide = Input.GetAxis("Horizontal") * currentSpeed;

            move = transform.right * moveSide + transform.forward * moveForward;
            characterController.Move(move * Time.deltaTime);

            // Controle de Visão (Câmera)
            float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

            rotationY += mouseX;  // Rotação horizontal
            rotationX -= mouseY;  // Rotação vertical

            // Limitar a rotação vertical
            rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

            // Aplicar a rotação ao jogador (apenas no eixo Y para evitar giro em torno de seu próprio eixo)
            transform.localRotation = Quaternion.Euler(0f, rotationY, 0f);

            // Aplicar rotação à câmera (controle de visão vertical)
            cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

            // Gravidade
            if (characterController.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f; // Valor pequeno para manter o jogador preso ao chão
            }
            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }

    }
}
