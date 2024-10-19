using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventarioManager : MonoBehaviour
{
    public Objetos[] slots; // Slots do inventário
    public Image[] slotImage; // Imagens dos slots
    public int[] slotAmount; // Quantidade de itens nos slots
    private InterfaceManager iController;

    private void Start()
    {
        iController = FindObjectOfType<InterfaceManager>();
    }
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hit, 5f))
        {
            Debug.Log("Hit: " + hit.collider.name); // Verificando o que o Raycast está acertando

            if (hit.collider.tag == "Object")
            {
                iController.itemText.text = "[E] PARA COLETAR " + hit.transform.GetComponent<TipoObjeto>().tipoObjeto.itemName;

                Debug.Log("Objeto com tag 'Object' detectado!"); // Confirmando a detecção correta do objeto

                if (Input.GetKeyDown(KeyCode.E))
                {
                    for (int i = 0; i < slots.Length; i++)
                    {
                        // Verifica se o slot está vazio ou se o objeto é o mesmo que já está no slot
                        if (slots[i] == null || slots[i] == hit.transform.GetComponent<TipoObjeto>().tipoObjeto)
                        {
                            // Adiciona o objeto no slot
                            slots[i] = hit.transform.GetComponent<TipoObjeto>().tipoObjeto;
                            slotAmount[i]++;

                            // Atualiza a imagem do slot
                            slotImage[i].sprite = slots[i].itemSprite;

                            // Destroi o objeto coletado
                            Destroy(hit.transform.gameObject);
                            break;
                        }
                    }
                }
            }
            else if (hit.collider.tag != "Object")
            {
                iController.itemText.text = null;
            }
        }
    }
}
