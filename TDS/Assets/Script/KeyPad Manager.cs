using TMPro;
using UnityEngine;

public class KeyPadManager : MonoBehaviour
{
    public float distanciaMax = 5f;
    public GameObject keypadUI;  // A UI do Keypad que será ativada
    public Transform playerCamera;  // A câmera do jogador
    public TextMeshProUGUI textoInteracao;

    private bool isNearKeypad = false;
    private bool isPuzzleActive = false;

    void Update()
    {
        if (VerificarInteracaoComKeypad())
        {
            isNearKeypad = true;
            AtualizarTexto();
            // Pressione "E" para interagir
            if (Input.GetKeyDown(KeyCode.E) && !isPuzzleActive)
            {
                OpenKeypad();
            }
        }
        else
        {
            isNearKeypad = false;
        }
    }

    bool VerificarInteracaoComKeypad()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distanciaMax))
        {
            if (hit.collider.CompareTag("KeyPad"))
            {
                return true; // Jogador pode interagir
            }
        }
        return false; // Jogador não pode interagir
    }

    void OpenKeypad()
    {
        isPuzzleActive = true;
        keypadUI.SetActive(true);  // Mostra a UI do Keypad

    }



    private void AtualizarTexto()
    {
        textoInteracao.text = "[E] INTERAGIR";
    }

}
