using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningUnitAttributes: MonoBehaviour
{
    public MiningUnitType unitType;
    public Material currentMaterial;
    public int currentTileValue;
    public int rowPosition;
    public int columnPosition;

    public void SetTileAttributes(MiningUnitType unitType, Material currentMaterial, int currentTileValue)
    {
        this.unitType = unitType;
        this.currentMaterial = currentMaterial;
        this.currentTileValue = currentTileValue;
    }
}
