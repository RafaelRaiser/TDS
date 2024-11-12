//testem o codigo antes de aplicar
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorPuzzle : MonoBehaviour
{
    public List<Button> colorButtons;
    public List<Color> buttonColors;
    public Button closeButton;
    public GameObject puzzleUI;
    public TextMeshProUGUI feedbackText;

    private List<int> sequence = new List<int>();
    private int playerIndex = 0;
    private bool isPlayerTurn = false;
    private bool isPuzzleActive = false;

    void Start()
    {
        puzzleUI.SetActive(false);

        for (int i = 0; i < colorButtons.Count; i++)
        {
            colorButtons[i].image.color = buttonColors[i];
            int index = i;
            colorButtons[i].onClick.AddListener(() => PlayerInput(index));
        }

        closeButton.onClick.AddListener(ClosePuzzleUI);
    }

    void Update()
    {
        if (isPuzzleActive && Input.GetKeyDown(KeyCode.E))
        {
            OpenPuzzleUI();
            StartCoroutine(GenerateSequence());
        }
    }

    public void ActivatePuzzle()
    {
        isPuzzleActive = true;
    }

    void OpenPuzzleUI()
    {
        puzzleUI.SetActive(true);
        feedbackText.text = "Observe a sequência!";
    }

    void ClosePuzzleUI()
    {
        puzzleUI.SetActive(false);
        isPuzzleActive = false;
    }

    IEnumerator GenerateSequence()
    {
        playerIndex = 0;
        sequence.Add(Random.Range(0, colorButtons.Count));
        isPlayerTurn = false;

        for (int i = 0; i < sequence.Count; i++)
        {
            int colorIndex = sequence[i];
            HighlightButton(colorButtons[colorIndex]);
            yield return new WaitForSeconds(1f);
            ResetButton(colorButtons[colorIndex]);
            yield return new WaitForSeconds(0.5f);
        }

        isPlayerTurn = true;
        feedbackText.text = "Sua vez! Repita a sequência.";
    }

    void HighlightButton(Button button)
    {
        button.image.color = Color.white;
    }

    void ResetButton(Button button)
    {
        int index = colorButtons.IndexOf(button);
        button.image.color = buttonColors[index];
    }

    void PlayerInput(int colorIndex)
    {
        if (!isPlayerTurn) return;

        if (sequence[playerIndex] == colorIndex)
        {
            playerIndex++;
            if (playerIndex >= sequence.Count)
            {
                feedbackText.text = "Correto! Preparando a próxima sequência.";
                isPlayerTurn = false;
                StartCoroutine(GenerateSequence());
            }
        }
        else
        {
            feedbackText.text = "Errou! Tente novamente.";
            sequence.Clear();
            StartCoroutine(GenerateSequence());
        }
    }
}
