using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PortaManager : MonoBehaviour
{
    public float distanciaMax = 10f;
    public Transform playerCamera;
    public TextMeshProUGUI interagirTexto;

    private IInteragivel objetoAtual;

    private void Update()
    {
        if (VerificarInteracaoComObjeto())
        {
            MostrarTextoInteracao();

            if (Input.GetKeyDown(KeyCode.E))
            {
                objetoAtual.Interact();
            }
        }
        else
        {
            OcultarTextoInteracao();
        }
    }

    bool VerificarInteracaoComObjeto()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distanciaMax))
        {
            // Tenta obter o componente que implementa IInteragivel
            IInteragivel interagivel = hit.collider.GetComponent<IInteragivel>();

            if (interagivel != null)
            {
                objetoAtual = interagivel;  // Armazena o objeto atual
                return true; // Jogador pode interagir
            }
        }
        objetoAtual = null;
        return false; // Jogador não pode interagir
    }


    void MostrarTextoInteracao()
    {
        if (objetoAtual != null)
        {
            interagirTexto.enabled = true;
            interagirTexto.text = objetoAtual.TextInteragivel;
        }
    }

    void OcultarTextoInteracao()
    {
        interagirTexto.enabled = false;
    }
}
