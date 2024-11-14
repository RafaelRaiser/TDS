// GeniusPuzzle.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GeniusPuzzle : MonoBehaviour
{
    public int score = 0;
    public int targetScore = 666; // Pontuação necessária para concluir o puzzle
    public float timeLimit = 60f; // Tempo limite para o puzzle em segundos
    private float currentTime;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public GameObject quitButton;

    public Button[] colorButtons; // Botões das cores do jogo
    private List<int> sequence = new List<int>(); // Sequência de cores para seguir
    private int playerStep = 0; // Passo atual do jogador na sequência
    private bool isPlayerTurn = false;

    private int pointsPerCorrect = 50; // Pontos por acerto
    private int initialSequenceLength = 3; // Número inicial de cores na sequência
    private float timeBonusPerCorrect = 5f; // Bônus de tempo por acerto
    private float timePenaltyPerError = 10f; // Penalidade de tempo por erro

    void Start()
    {
        quitButton.SetActive(false);
        gameObject.SetActive(false); // Começa desativado
    }

    public void ActivatePuzzle()
    {
        gameObject.SetActive(true);
        StartPuzzle();

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        quitButton.SetActive(true);
    }

    public void DeactivatePuzzle()
    {
        gameObject.SetActive(false);

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        quitButton.SetActive(false);
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI();

            if (currentTime <= 0)
            {
                currentTime = 0;
                GameOver();
            }
        }
    }

    private void StartPuzzle()
    {
        currentTime = timeLimit;
        score = 0;
        UpdateScoreUI();
        UpdateTimerUI();

        sequence.Clear();
        GenerateNewSequence(initialSequenceLength); // Gera uma sequência inicial
        StartCoroutine(ShowSequence()); // Mostra a sequência imediatamente
    }

    private void GenerateNewSequence(int length)
    {
        for (int i = 0; i < length; i++)
        {
            int randomIndex = Random.Range(0, colorButtons.Length);
            sequence.Add(randomIndex);
        }
    }

    private IEnumerator ShowSequence()
    {
        isPlayerTurn = false;
        yield return new WaitForSeconds(1f);

        foreach (int index in sequence)
        {
            var buttonImage = colorButtons[index].GetComponent<Image>();
            var originalColor = buttonImage.color;

            buttonImage.color = new Color(originalColor.r * 0.5f, originalColor.g * 0.5f, originalColor.b * 0.5f); // Escurece a cor
            yield return new WaitForSeconds(0.5f);

            buttonImage.color = originalColor; // Restaura a cor original
            yield return new WaitForSeconds(0.2f);
        }

        isPlayerTurn = true;
        playerStep = 0;
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.Ceil(currentTime).ToString();
        }
    }

    public void OnColorButtonClick(Button button)
    {
        if (!isPlayerTurn) return;

        int buttonIndex = System.Array.IndexOf(colorButtons, button);
        if (buttonIndex == sequence[playerStep])
        {
            playerStep++;
            score += pointsPerCorrect;
            currentTime += timeBonusPerCorrect;
            UpdateScoreUI();
            UpdateTimerUI();

            if (playerStep >= sequence.Count)
            {
                if (score >= targetScore)
                {
                    PuzzleComplete();
                }
                else
                {
                    sequence.Add(Random.Range(0, colorButtons.Length)); // Adiciona uma nova cor à sequência para aumentar a dificuldade
                    StartCoroutine(ShowSequence()); // Mostra a sequência atualizada
                }
            }
        }
        else
        {
            currentTime -= timePenaltyPerError; // Penalidade de tempo por erro
            UpdateTimerUI();
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("O tempo acabou ou a sequência foi incorreta. Tente novamente.");
        DeactivatePuzzle();
    }

    private void PuzzleComplete()
    {
        Debug.Log("Parabéns! Você completou o puzzle com 666 pontos.");
        DeactivatePuzzle();
    }
}

