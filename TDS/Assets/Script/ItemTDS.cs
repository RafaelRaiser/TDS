using UnityEngine;

public class ItemTDS : MonoBehaviour
{
    public string itemName;  // Nome do item
    public Sprite icon;  // �cone para exibir no invent�rio
    public bool isStackable;  // Se o item pode ser empilhado no invent�rio

    public virtual void Use()
    {
        // L�gica de uso do item
        Debug.Log("Usando " + itemName);
    }
}