using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    public GameObject toggleGameModeButton;

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

    

    // Start is called before the first frame update
    void Start()
    {
        activeIcon.GetComponent<Image>().sprite = pickAxeIcon;
        iconOffset = pickAxeOffset;
        Cursor.visible = false;
        currentGameModeText.text = "Current Mode: Extract";
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
        scoreText.text = "Score: " + GameStatManager.score;
    }
    public void UpdateExtractionsRemaining()
    {
        extractsRemainingText.text = "Extractions Remaining: " + GameStatManager.extractionsRemaining;
    }
    public void UpdateScansRemaining()
    {
        scansRemainingText.text = "Scans Remaining: " + GameStatManager.scansRemaining;
    }
    public void UpdateRecentExtractionMessage()
    {
        recentExtractionsMessageText.text = "Congratulations! You received " + GameStatManager.score + " gold from your recent extraction!";
    }

    public void OnResetGameButton()
    {
        GameStatManager.ResetAllGameStats();
        ChangeToExtractMode();
        UpdateScoreText();
        UpdateExtractionsRemaining();
        UpdateScansRemaining();
    }

}
