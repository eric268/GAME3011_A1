using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningUnitAttributes: MonoBehaviour
{
    public Material startingResourceMaterial;
    public Material maxResourceMaterial;
    public Material halfResourceMaterial;
    public Material QuarterResourceMaterial;
    public Material minimalResourceMaterial;

    public int maxResourceValue;
    public int halfResourceValue;
    public int quarterResourceValue;
    public int minimalResourceValue;

    public int rowPosition;
    public int columnPosition;

    public MiningUnitType unitType;
}
