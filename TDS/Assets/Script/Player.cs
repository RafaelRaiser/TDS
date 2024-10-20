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

    public float moveSpeed = 2f;  // Valor maior para garantir que o movimento seja percept�vel
    public float lookSensitivity = 2f;
    public float slowSpeed = 1f;
    public float gravity = -9.81f;

    public float minVerticalAngle = -90f;
    public float maxVerticalAngle = 90f;

    private Vector3 velocity;
    private float rotationX = 0f;
    private float rotationY = 0f;

    private CharacterController characterController;
    private Transform cameraTransform;

    private bool movimentar = true;

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
            float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? slowSpeed : moveSpeed;  // Usa slowSpeed com Shift
            float moveForward = Input.GetAxis("Vertical") * currentSpeed;
            float moveSide = Input.GetAxis("Horizontal") * currentSpeed;

            move = transform.right * moveSide + transform.forward * moveForward;
            characterController.Move(move * Time.deltaTime);

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
