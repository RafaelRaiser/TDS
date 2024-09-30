using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadPuzzle : MonoBehaviour
{
    public string correctCode = "1234";
    private string playerInput = "";
    Collider2D colidder;
    Porta script;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Porta"))
        {
            script = other.GetComponent<Porta>();
        }
    }


    public void AddDigit(string digit)
    {
        playerInput += digit;

        if (playerInput.Length == 4)
        {
            CheckCode();
        }
    }

    public void ResetCode()
    {
        playerInput = "";
    }

    void CheckCode()
    {
        if (playerInput == correctCode)
        {
            //doorControlScript.isOpen = true;  // Abre a porta
            Debug.Log("Code is correct! Door opened.");
            if (script != null)
            {
                script.AbrirPorta();
            }
        }
        else
        {
            Debug.Log("Incorrect code!");
        }

        ResetCode();  // Reseta o código depois da tentativa
    }

    public void ClosePuzzle()
    {
        //Chame o método CloseKeypad do script KeypadInteraction para fechar a UI
        FindObjectOfType<KeypadInteraction>().CloseKeypad();
    }
}

