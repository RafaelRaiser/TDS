using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flaslight : MonoBehaviour
{
    private Light lanternaLuz;
    public KeyCode teclaLanterna = KeyCode.F;

    void Start()
    {
        lanternaLuz = GetComponent<Light>();
    }

    void Update()
    {
        if (InventarioManager.instance.PesquisarItem("Lanterna") && Input.GetKeyDown(teclaLanterna))
        {
            lanternaLuz.enabled = !lanternaLuz.enabled;
        }
    }
}
