using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridGenerator : MonoBehaviour
{
    private static GridGenerator instance;
    public static int numberOfRows;
    public static int numberOfColumns;
    public GameObject[,] miningTileArray;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private GridGenerator()
    {
        numberOfColumns = 32;
        numberOfRows = 32;
        miningTileArray = new GameObject[numberOfColumns, numberOfRows];
    }

    public static GridGenerator GetInstance()
    {
        if (instance == null)
            instance = new GridGenerator();

        return instance;
    }
}
