using UnityEngine;

public class KeypadInteraction : MonoBehaviour
{
    public float interactionDistance = 5f;
    public GameObject keypadUI;  // A UI do Keypad que será ativada
    public Transform playerCamera;  // A câmera do jogador

    private bool isNearKeypad = false;
    private bool isPuzzleActive = false;

    void Update()
    {
        // Raycast da câmera do player
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag("Keypad"))  // Certifique-se de adicionar a tag "Keypad" ao objeto
            {
                isNearKeypad = true;
                // Pressione "E" para interagir
                if (Input.GetKeyDown(KeyCode.E) && !isPuzzleActive)
                {
                    OpenKeypad();
                }
            }
        }
        else
        {
            isNearKeypad = false;
        }
    }

    void OpenKeypad()
    {
        isPuzzleActive = true;
        keypadUI.SetActive(true);  // Mostra a UI do Keypad
        Cursor.lockState = CursorLockMode.None;  // Libera o cursor para a interação
        Cursor.visible = true;
    }

    public void CloseKeypad()
    {
        isPuzzleActive = false;
        keypadUI.SetActive(false);  // Esconde a UI do Keypad
        Cursor.lockState = CursorLockMode.Locked;  // Bloqueia o cursor novamente
        Cursor.visible = false;
    }
}
