using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileManager : MonoBehaviour
{
    private MiningUnitAttributes tileAttributes;
    // Start is called before the first frame update
    void Start()
    {
        tileAttributes = GetComponent<MiningUnitAttributes>();
    }

    private void OnMouseDown()
    {
        switch (GridManager.currentGameMode)
        {
            case MiningGameModes.SCAN_MODE:
                TileSelectedInScanMode();
                break;
            case MiningGameModes.EXTRACT_MODE:
                TileSelectedInExtractMode();
                break;
        }
        print("Down");
    }
    private void OnMouseEnter()
    {
        if (tileAttributes.tileHasBeenScanned)
        {
            GetComponent<MeshRenderer>().material = tileAttributes.highlightedMaterial;
        }
        else
        {
            GetComponent<MeshRenderer>().material = tileAttributes.startingHighlightedResourceMaterial;
        }
    }
    private void OnMouseExit()
    {
        if (tileAttributes.tileHasBeenScanned)
        {
            GetComponent<MeshRenderer>().material = tileAttributes.scannedMaterial;
        }
        else
        {
            GetComponent<MeshRenderer>().material = tileAttributes.startingResourceMaterial;
        }
    }

    private void TileSelectedInScanMode()
    {
        int row = GetComponent<MiningUnitAttributes>().rowPosition;
        int col = GetComponent<MiningUnitAttributes>().columnPosition;
    }

    private void TileSelectedInExtractMode()
    {

    }
}
