using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;
using UnityEngine.InputSystem.HID;

public class PortaScript : MonoBehaviour
{
    public float distanciaMax = 10f;

    public Transform playerCamera;

    Animation portaAnimacao; 

    public TextMeshProUGUI interagirTexto;

    bool portaAberta = false;

    string nomeObjetoInteragivel;

    string nomeSemNumero;

    private void Update()
    {
        if (VerificarInteracaoComPorta())
        {
            MostrarTextoInteracao();

            if (Input.GetKeyDown(KeyCode.E))
            {
                AlternarPorta();
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
                nomeObjetoInteragivel = hit.collider.gameObject.name;
                nomeSemNumero = nomeObjetoInteragivel.TrimEnd('0', '1', '2', '3', '4', '5', '6', '7', '8', '9');
                portaAnimacao = hit.collider.GetComponentInParent<Animation>();
                Debug.Log(nomeSemNumero);
                return true; // Jogador pode interagir
            }
        }
        return false; // Jogador não pode interagir
    }

    void MostrarTextoInteracao()
    {
        interagirTexto.enabled = true;
        interagirTexto.text = portaAberta ? "[E] Fechar" : "[E] Abrir";
    }

    void OcultarTextoInteracao()
    {
        interagirTexto.enabled = false;
    }

    void AlternarPorta()
    {
        portaAberta = !portaAberta; // Alterna entre abrir e fechar

        if (portaAberta)
        {
            portaAnimacao.Play( nomeSemNumero + "_Open"); // Toca animação de abrir
        }
        else
        {
            portaAnimacao.Play(nomeSemNumero + "_Close"); // Toca animação de fechar
        }
    }
}
