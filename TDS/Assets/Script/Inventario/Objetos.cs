using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "Inventory Objects/Create New")]
public class Objetos : ScriptableObject
{
    public string itemName; // Nome do item
    public Sprite itemSprite; // Sprite associado ao item
}
