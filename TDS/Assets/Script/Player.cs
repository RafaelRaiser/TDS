using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lookSensitivity = 2f;
    public float slowSpeed = 2f;
    public float gravity = -9.81f;
   //CanvasActivator canvasActivator;

    public float minVerticalAngle = -90f;
    public float maxVerticalAngle = 90f;

    private Vector3 velocity;
    private float rotationX = 0f;
    private float rotationY = 0f;

    private CharacterController characterController;
    private Transform cameraTransform;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
       //canvasActivator = GameObject.Find("Canvas").GetComponent<CanvasActivator>();
    }

    void Update()
    {
        // Movimentação
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? slowSpeed : moveSpeed;
        float moveForward = Input.GetAxis("Vertical") * currentSpeed;
        float moveSide = Input.GetAxis("Horizontal") * currentSpeed;

        Vector3 move = transform.right * moveSide + transform.forward * moveForward;
        characterController.Move(move * Time.deltaTime);
        if (move == Vector3.zero)
        {
          //  canvasActivator.HideCanvas();
        }
        // Controle de Visão
        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        // Aplicar rotação ao jogador e à câmera
        transform.localRotation = Quaternion.Euler(0f, rotationY, 0f);
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
