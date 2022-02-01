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
    // Start is called before the first frame update
    void Start()
    {
        extractButton.GetComponent<Image>().color = inactiveColor;
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
