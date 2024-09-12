﻿using UnityEngine;

public class CanvasActivator : MonoBehaviour
{
    public GameObject rawImage; // Refer�ncia ao GameObject do Canvas que voc� deseja controlar


    void Update()
    {
        // Verifica se a tecla Shift est� sendo segurada
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            HideCanvas();
        }
        else
        {
            ShowCanvas();
        }

    }

    public void HideCanvas()
    {
        rawImage.SetActive(false); // Desativa o Canvas
    }

    public void ShowCanvas()
    {
        rawImage.SetActive(true); // Ativa o Canvas
    }
}