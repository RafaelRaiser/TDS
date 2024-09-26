using UnityEngine;

public class ItemTDS : MonoBehaviour
{
    public string itemName;  // Nome do item
    public Sprite icon;  // Ícone para exibir no inventário
    public bool isStackable;  // Se o item pode ser empilhado no inventário

    public virtual void Use()
    {
        // Lógica de uso do item
        Debug.Log("Usando " + itemName);
    }
}