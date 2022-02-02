using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

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

    public GameObject activeIcon;
    public Sprite pickAxeIcon;
    public Sprite shovelIcon;

    public Vector3 iconOffset;
    public Vector3 shovelOffset;
    public Vector3 pickAxeOffset;

    // Start is called before the first frame update
    void Start()
    {
        extractButton.GetComponent<Image>().color = inactiveColor;
        iconOffset = shovelOffset;
        Cursor.visible = false;
    }

    private void Update()
    {
        activeIcon.transform.position = Input.mousePosition + iconOffset;
    }

    public void OnScanButtonPressed()
    {
        GridManager.currentGameMode = MiningGameModes.SCAN_MODE;
        extractButton.GetComponent<Image>().color = inactiveColor;
        scanButton.GetComponent<Image>().color = activeColor;
        activeIcon.GetComponent<Image>().sprite = shovelIcon;
        iconOffset = shovelOffset;
    }

    public void OnExtractButtonPressed()
    {
        GridManager.currentGameMode = MiningGameModes.EXTRACT_MODE;
        extractButton.GetComponent<Image>().color = activeColor;
        scanButton.GetComponent<Image>().color = inactiveColor;
        activeIcon.GetComponent<Image>().sprite = pickAxeIcon;
        iconOffset = pickAxeOffset;
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
