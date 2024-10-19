using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{

    public GameObject invetoryPanel;

    bool InventarioAtivado;
    void Start()
    {
        
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
