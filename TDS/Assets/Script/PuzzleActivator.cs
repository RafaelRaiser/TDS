// PuzzleActivator.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleActivator : MonoBehaviour
{
    [SerializeField] private GeniusPuzzle puzzle;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Pressionar "E" ativa o puzzle imediatamente
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 5f))
            {
                if (hit.collider.CompareTag("Puzzle"))
                {
                    puzzle.ActivatePuzzle();
                }
            }
        }
    }
}
