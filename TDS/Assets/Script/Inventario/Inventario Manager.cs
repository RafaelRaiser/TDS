using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public Objetos[] slots;

    public Image[] slotImage;

    public int[] slotAmount;

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hit, 5f))
        {
            if (hit.collider.tag == "Object")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    for (int i = 0; i < slots.Length; i++)
                    {
                        if (slots[i] == null || slots[i].name == hit.transform.GetComponent<TipoObjeto>().TipoObjeto.name)
                        {
                            slots[i] = hit.transform.GetComponent<TipoObjeto>().tipoObjeto;
                            slotAmount[i] ++;
                            slotImag[i].sprite = slots[i].itemSprite;

                            Destroy(hit.transform.GameObject);
                            break;
                        }
                    }
                }
            }
        }
    }






}
