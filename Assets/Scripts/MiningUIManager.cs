using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MiningUIManager : MonoBehaviour
{
    public GridManager gridManager;
    public Toggle showRemainingTilesToggle;

    public Color activeColor;
    public Color inactiveColor;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI extractsRemainingText;
    public TextMeshProUGUI scansRemainingText;
    public TextMeshProUGUI recentExtractionsMessageText;
    public TextMeshProUGUI currentGameModeText;

    public GameObject activeIcon;
    public Sprite pickAxeIcon;
    public Sprite shovelIcon;

    public Vector3 iconOffset;
    public Vector3 shovelOffset;
    public Vector3 pickAxeOffset;

    public static bool showTilesOnGameOver;

    

    // Start is called before the first frame update
    void Start()
    {
        activeIcon.GetComponent<Image>().sprite = pickAxeIcon;
        gridManager.UpdateUIText = UpdateUI;
        iconOffset = pickAxeOffset;
        currentGameModeText.text = "Current Mode: Extract";
        showTilesOnGameOver = showRemainingTilesToggle.isOn;
    }

    private void Update()
    {
        activeIcon.transform.position = Input.mousePosition + iconOffset;
    }

    public void ToggleGameModeButtonPressed()
    {
        if (GameStatManager.currentGameMode == MiningGameModes.EXTRACT_MODE)
            ChangeToScanMode();
        else
            ChangeToExtractMode();
    }

    public void ResetGameButton()
    {
        gridManager.ResetGrid();
        GameStatManager.ResetAllGameStats();
        ChangeToExtractMode();
        UpdateUI();
        recentExtractionsMessageText.text = "";
    }
    public void ChangeToScanMode()
    {
        GameStatManager.currentGameMode = MiningGameModes.SCAN_MODE;
        activeIcon.GetComponent<Image>().sprite = shovelIcon;
        iconOffset = shovelOffset;
        currentGameModeText.text = "Current Mode: Scan";
    }

    public void ChangeToExtractMode()
    {
        GameStatManager.currentGameMode = MiningGameModes.EXTRACT_MODE;
        activeIcon.GetComponent<Image>().sprite = pickAxeIcon;
        iconOffset = pickAxeOffset;
        currentGameModeText.text = "Current Mode: Extract";
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Total Score: " + GameStatManager.score;
    }
    public void UpdateExtractionsRemaining()
    {
        extractsRemainingText.text = "Extracts Remaining: " + GameStatManager.extractionsRemaining;
    }
    public void UpdateScansRemaining()
    {
        scansRemainingText.text = "Scans Remaining: " + GameStatManager.scansRemaining;
    }
    public void UpdateRecentExtractionMessage()
    {
        recentExtractionsMessageText.text = "Congratulations! You received " + GameStatManager.recentExtractGoldEarned + " gold from your recent extraction!";
    }

    public void UpdateUI()
    {
        UpdateScoreText();
        UpdateExtractionsRemaining();
        UpdateScansRemaining();
    }
    
    public void OnShowRemainingTilesTogglePressed()
    {
        showTilesOnGameOver = showRemainingTilesToggle.isOn;
    }

    public void OnLeaveButtonPressed()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void DisplayFinalScoreMessage()
    {
        recentExtractionsMessageText.text = "Game Over! You received " + GameStatManager.score + " gold from playing Mining Mayhem!";
    }

}
