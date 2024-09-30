using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PortaManager : MonoBehaviour
{
    public float distanciaMax = 10f;
    public Transform playerCamera;
    public TextMeshProUGUI interagirTexto;

    private Porta portaAtual;

    private void Update()
    {
        if (VerificarInteracaoComPorta())
        {
            MostrarTextoInteracao();

            if (Input.GetKeyDown(KeyCode.E))
            {
                portaAtual.AlternarPorta();
            }
        }
        else
        {
            OcultarTextoInteracao();
        }
    }

    bool VerificarInteracaoComPorta()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distanciaMax))
        {
            if (hit.collider.CompareTag("Porta"))
            {
                portaAtual = hit.collider.GetComponent<Porta>();
                return true; // Jogador pode interagir
            }
        }
        portaAtual = null;
        return false; // Jogador não pode interagir
    }

    void MostrarTextoInteracao()
    {
        if (portaAtual != null)
        {
            interagirTexto.enabled = true;
            interagirTexto.text = portaAtual.EstaAberta() ? "[E] Fechar" : "[E] Abrir";
        }
    }

    void OcultarTextoInteracao()
    {
        interagirTexto.enabled = false;
    }
}
