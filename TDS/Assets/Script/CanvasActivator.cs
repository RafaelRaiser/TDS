using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasActivator : MonoBehaviour
{
    public GameObject rawImage; // Refer�ncia ao GameObject do Canvas que voc� deseja controlar

    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void Update()
    {
        // Verifica se a tecla Shift est� sendo segurada
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) || FindObjectOfType<Player>().move == Vector3.zero)
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