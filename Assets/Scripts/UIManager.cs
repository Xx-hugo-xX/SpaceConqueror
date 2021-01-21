using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private LevelManager lM;
    [SerializeField] private IntroLevelManager iLM;
    [SerializeField] private GameObject levelStartCountdownPanel;
    [SerializeField] private GameObject introStartCountdownPanel;
    [SerializeField] private GameObject introMessagePanel;

    [SerializeField] private TextMeshProUGUI preMatchRoundTimer;
    [SerializeField] private TextMeshProUGUI preIntroRoundTimer;

    [SerializeField] private int finalScoreDuration;

    private bool isCountingDown = false;
    private int finalScoreTimer;

    [SerializeField] private GameObject currentScorePanel;
    [SerializeField] public TextMeshProUGUI currentScoreDisplay;

    [SerializeField] private GameObject levelPanel;
    [SerializeField] public TextMeshProUGUI levelDisplay;

    [SerializeField] public TextMeshProUGUI handednessDisplay;
    private int playerScore = 0;

    [SerializeField] private GameObject finalScorePanel;
    [SerializeField] public TextMeshProUGUI finalScoreDisplay;
    [SerializeField] public TextMeshProUGUI finalLevelDisplay;


    Handedness pHandedness = Handedness.Right;


    private void Start()
    {
        
    }

    private void Update()
    {
        if (iLM.enabled) UpdatePreIntroTimer();

        if (lM.enabled) UpdatePreMatchTimer();

        UpdateCurrentScore();
        UpdateHandDisplay();
        if (lM.hasLevelProgression) UpdateLevelPanel();

        if (finalScorePanel.activeSelf && !isCountingDown && Input.anyKey)
            lM.RestartLevel();
    }

    private void UpdatePreMatchTimer()
    {
        if (lM.isCountingDown)
        {
            if (!levelStartCountdownPanel.activeSelf) levelStartCountdownPanel.SetActive(true);

            preMatchRoundTimer.text = lM.preMatchTimer.ToString();
        }

        else
        {
            if (levelStartCountdownPanel.activeSelf) levelStartCountdownPanel.SetActive(false);
        }
    }

    private void UpdatePreIntroTimer()
    {
        if (iLM.isCountingDown)
        {
            if (!introStartCountdownPanel.activeSelf) introStartCountdownPanel.SetActive(true);

            preIntroRoundTimer.text = iLM.preMatchTimer.ToString();
        }

        else
        {
            if (introStartCountdownPanel.activeSelf) introStartCountdownPanel.SetActive(false);
        }
    }

    private void UpdateCurrentScore()
    {
        if (lM.pB.GetScore() != playerScore)
        {
            playerScore = lM.pB.GetScore();

            currentScoreDisplay.text = $"Pontos: {playerScore}";
        }
    }

    public void ShowFinalScore()
    {
        lM.DestroyAllShipsAndLasers(true);
        finalScoreDisplay.text = $"Pontuação Final: {lM.pB.GetScore()}";
        if (lM.enabled && lM.hasLevelProgression) finalLevelDisplay.text = $"Nível Atingido: {lM.GetCurrentLevel()}";
        else finalLevelDisplay.text = "";
        currentScorePanel.SetActive(false);
        finalScorePanel.SetActive(true);
        StartCoroutine(FinalScoreCountdown());
    }

    public void UpdateHandDisplay()
    {
        string currentHandS;
        if (pHandedness == Handedness.Right) currentHandS = "Destro";
        else currentHandS = "Esquerdino";

        if (currentHandS != handednessDisplay.text) handednessDisplay.text = currentHandS;
    }

    public void SwitchHandedness()
    {
        switch (pHandedness)
        {
            case Handedness.Left:
                pHandedness = Handedness.Right;
                break;
            case Handedness.Right:
                pHandedness = Handedness.Left;
                break;
            default:
                break;
        }
    }

    public Handedness GetHandedness() => pHandedness;

    public void HideIntroMessagePanel()
    {
        introMessagePanel.SetActive(false);
    }

    public void ShowLevelPanel()
    {
        levelPanel.SetActive(true);
    }

    public void UpdateLevelPanel()
    {
        levelDisplay.text = $"Nível {lM.GetCurrentLevel()}";
    }

    #region OBSOLETE

    private IEnumerator FinalScoreCountdown()
    {
        isCountingDown = true;

        finalScorePanel.SetActive(true);

        finalScoreTimer = finalScoreDuration;

        while (finalScoreTimer > 0)
        {
            yield return new WaitForSeconds(1);
            finalScoreTimer--;
        }
        isCountingDown = false;
    }

    #endregion
}
