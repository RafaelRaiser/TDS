using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTDS : MonoBehaviour
{
    public List<ItemTDS> items = new List<ItemTDS>(); // Lista de todos os itens

    // Adiciona um item ao inventário
    public void AddItem(ItemTDS newItem)
    {
        items.Add(newItem);
        Debug.Log("Item added: " + newItem.itemName);
    }

    // Remove um item do inventário
    public void RemoveItem(ItemTDS item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log("Item removed: " + item.itemName);
        }
    }

    // Usa um item (pode ser estendido para selecionar um item da lista)
    public void UseItem(ItemTDS item)
    {
        if (items.Contains(item))
        {
            item.Use();
        }
    }
}