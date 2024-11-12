//Mesma coisa do Color Puzzle
using UnityEngine;

public class PuzzleActivator : MonoBehaviour
{
    private ColorPuzzle geniusPuzzle;

    void Start()
    {
        geniusPuzzle = FindObjectOfType<ColorPuzzle>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            geniusPuzzle.ActivatePuzzle();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            geniusPuzzle.isPuzzleActive = false;
        }
    }
}
