using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public Text itemText;

    public GameObject invetoryPanel;

    bool InventarioAtivado;
    void Start()
    {
        itemText.text = null;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InventarioAtivado = !InventarioAtivado;
            invetoryPanel.SetActive(InventarioAtivado);
        }
        if (InventarioAtivado)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
