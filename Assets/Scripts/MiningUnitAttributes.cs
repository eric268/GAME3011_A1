using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningUnitAttributes: MonoBehaviour
{
    public MiningUnitType unitType;
    public Material startingResourceMaterial;
    public Material startingHighlightedResourceMaterial;
    public Material scannedMaterial;
    public Material highlightedMaterial;
    public int currentTileValue;
    public int rowPosition;
    public int columnPosition;
    public bool tileHasBeenScanned;

    public void SetTileAttributes(MiningUnitType unitType, Material currentMaterial, Material highlightedMaterial, int currentTileValue)
    {
        this.unitType = unitType;
        this.scannedMaterial = currentMaterial;
        this.highlightedMaterial = highlightedMaterial;
        this.currentTileValue = currentTileValue;
    }

    public void SetMaterial(Material mat)
    {
        GetComponent<MeshRenderer>().material = mat;
    }
}
