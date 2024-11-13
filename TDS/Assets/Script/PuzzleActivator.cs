using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleActivator : MonoBehaviour
{
    private GeniusPuzzle geniusPuzzle;

    void Start()
    {
        geniusPuzzle = FindObjectOfType<GeniusPuzzle>();
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
