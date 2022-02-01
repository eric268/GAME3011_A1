using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject scanButton;
    public GameObject extractButton;
    public Color activeColor;
    public Color inactiveColor;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI extractsRemainingText;
    public TextMeshProUGUI scansRemainingText;
    public TextMeshProUGUI recentExtractionsMessageText;

    // Start is called before the first frame update
    void Start()
    {
        extractButton.GetComponent<Image>().color = inactiveColor;
    }


    public void OnScanButtonPressed()
    {
        GridManager.currentGameMode = MiningGameModes.SCAN_MODE;
        extractButton.GetComponent<Image>().color = inactiveColor;
        scanButton.GetComponent<Image>().color = activeColor;
    }

    public void OnExtractButtonPressed()
    {
        GridManager.currentGameMode = MiningGameModes.EXTRACT_MODE;
        extractButton.GetComponent<Image>().color = activeColor;
        scanButton.GetComponent<Image>().color = inactiveColor;
    }

    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score;
    }
    public void UpdateExtractionsRemaining(int extractionsRemaining)
    {
        extractsRemainingText.text = "Extractions Remaining: " + extractionsRemaining;
    }
    public void UpdateScansRemaining(int scansRemaining)
    {
        scansRemainingText.text = "Scans Remaining: " + scansRemaining;
    }
    public void UpdateRecentExtractionMessage(int score)
    {
        recentExtractionsMessageText.text = "Congratulations! You received " + score + " gold from your recent extraction!";
    }

}
