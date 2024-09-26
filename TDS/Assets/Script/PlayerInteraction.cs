using System.Collections;
using System.Collections.Generic;
using UHFPS.Runtime;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 3f;
    public InventoryTDS inventory;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Tecla E para interagir
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange))
            {
                ItemTDS item = hit.transform.GetComponent<ItemTDS>();
                if (item != null)
                {
                    PickUpItem(item);
                }
            }
        }
    }

    void PickUpItem(ItemTDS item)
    {
        inventory.AddItem(item);
        Destroy(item.gameObject); // Remove o item da cena após ser coletado
    }
}