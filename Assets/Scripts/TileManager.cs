using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileManager : MonoBehaviour
{
    private MiningUnitAttributes tileAttributes;
    public GridManager gridManager;
    // Start is called before the first frame update
    void Start()
    {
        tileAttributes = GetComponent<MiningUnitAttributes>();
    }

    private void OnMouseDown()
    {
        switch (GameStatManager.currentGameMode)
        {
            case MiningGameModes.SCAN_MODE:
                TileSelectedInScanMode();
                break;
            case MiningGameModes.EXTRACT_MODE:
                TileSelectedInExtractMode();
                break;
        }
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
        if (GameStatManager.scansRemaining > 0)
        {
            int rowPosition = GetComponent<MiningUnitAttributes>().rowPosition;
            int columnPosition = GetComponent<MiningUnitAttributes>().columnPosition;
            gridManager.UpdateTilesFromScan(columnPosition, rowPosition);
            GameStatManager.scansRemaining--;
        }

    }

    private void TileSelectedInExtractMode()
    {
        if (GameStatManager.extractionsRemaining > 0)
        {
            GameStatManager.extractionsRemaining--;
            int rowPosition = GetComponent<MiningUnitAttributes>().rowPosition;
            int columnPosition = GetComponent<MiningUnitAttributes>().columnPosition;
            gridManager.UpdateTilesFromExtract(columnPosition, rowPosition);
        }
    }
}
