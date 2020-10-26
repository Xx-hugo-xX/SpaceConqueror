using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private LevelManager lM;

    [SerializeField] private GameObject LevelStartCountdownPanel;

    [SerializeField] private TextMeshProUGUI preMatchRoundTimer;

    [SerializeField] private int finalScoreDuration;

    private bool isCountingDown = false;
    private int finalScoreTimer;

    [SerializeField] private GameObject currentScorePanel;
    [SerializeField] public TextMeshProUGUI currentScoreDisplay;
    private int playerScore = 0;

    [SerializeField] private GameObject finalScorePanel;
    [SerializeField] public TextMeshProUGUI finalScoreDisplay;



    private void Start()
    {
        
    }

    private void Update()
    {
        UpdatePreMatchTimer();
        UpdateCurrentScore();

        if (finalScorePanel.activeSelf && !isCountingDown && Input.anyKey)
            lM.RestartLevel();
    }

    private void UpdatePreMatchTimer()
    {
        if (lM.isCountingDown)
        {
            if (!LevelStartCountdownPanel.activeSelf) LevelStartCountdownPanel.SetActive(true);

            preMatchRoundTimer.text = lM.preMatchTimer.ToString();
        }

        else
        {
            if (LevelStartCountdownPanel.activeSelf) LevelStartCountdownPanel.SetActive(false);
        }
    }

    private void UpdateCurrentScore()
    {
        if (lM.pB.GetScore() != playerScore)
        {
            playerScore = lM.pB.GetScore();

            currentScoreDisplay.text = $"Score: {playerScore}";
        }
    }

    public void ShowFinalScore()
    {
        lM.DestroyAllShipsAndLasers();
        finalScoreDisplay.text = $"Final Score: {lM.pB.GetScore()}";
        finalScorePanel.SetActive(true);
        StartCoroutine(FinalScoreCountdown());
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
