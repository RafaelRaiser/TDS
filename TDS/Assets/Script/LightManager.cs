using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Light luz; // A luz que vai piscar
    public float intensidadeMinima = 0.4f; // Intensidade mínima da luz
    public float intensidadeMaxima = 0.8f; // Intensidade máxima da luz
    public float intervaloMinimo = 0.1f; // Intervalo mínimo entre piscadas
    public float intervaloMaximo = 1.0f; // Intervalo máximo entre piscadas

    private float tempoParaProximaPiscada;

    void Start()
    {
        if (luz == null)
        {
            luz = GetComponent<Light>(); // Tenta pegar o componente Light no mesmo GameObject
        }

        DefinirProximaPiscada();
    }

    void Update()
    {
        // Verifica se é hora de piscar
        if (Time.time >= tempoParaProximaPiscada)
        {
            luz.enabled = !luz.enabled; // Alterna a luz entre ligada e desligada

            // Se a luz estiver ligada, define uma nova intensidade aleatória
            if (luz.enabled)
            {
                luz.intensity = Random.Range(intensidadeMinima, intensidadeMaxima);
            }

            DefinirProximaPiscada(); // Define o próximo intervalo de piscada
        }
    }

    void DefinirProximaPiscada()
    {
        tempoParaProximaPiscada = Time.time + Random.Range(intervaloMinimo, intervaloMaximo);
    }
}
