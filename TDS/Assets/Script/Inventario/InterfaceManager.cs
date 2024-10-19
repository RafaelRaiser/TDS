using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public GameObject inventoryPanel; // Nome corrigido
    public Text itemText;
    bool inventarioAtivado;

    void Start()
    {
        inventarioAtivado = false;
        inventoryPanel.SetActive(inventarioAtivado); // Certifica-se de que o inventário começa desativado
        itemText.text = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventarioAtivado = !inventarioAtivado;
            inventoryPanel.SetActive(inventarioAtivado);
        }

        if (inventarioAtivado)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
