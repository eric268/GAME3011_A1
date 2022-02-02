using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Vector3 startingPosition = Vector3.zero;
    public int numOfRows;
    public int numOfColumns;
    public GameObject miningUnitPrefab;
    public GameObject gridParentObject;
    public float gridSpacing;
 
    MiningUnitType[,] tileTypeArray;

    private int numberOfMaxValueTiles;

    [Header("Materials")]
    public Material startingMaterial;
    public Material maxValueMaterial;
    public Material halfValueMaterial;
    public Material quarterValueMaterial;
    public Material minimalValueMaterial;
    public Material hoveredStartingMaterial;
    public Material hoveredMaxValueMaterial;
    public Material hoveredHalfValueMaterial;
    public Material hoveredQuarterValueMaterial;
    public Material hoveredMinimalValueMaterial;



    [Header("Tile Values")]
    public int maxValue;
    public int halfValue;
    public int quarterValue;
    public int minimalValue;

    public static int scanExtent;

    [Header("Game Variables")]
    public static int playerScore;
    public int startingNumberOfScans;
    public static int scansRemaining;
    public int startingNumberOfExtracts;
    public static int extractsRemaining;
    public static int recentPointsEarned;
    public UIManager uIManager;


    // Start is called before the first frame update
    void Start()
    {
        GameStatManager.currentGameMode = MiningGameModes.EXTRACT_MODE;
        scanExtent = 2;
        numberOfMaxValueTiles = 15;
        tileTypeArray = new MiningUnitType[numOfRows, numOfColumns];
        scansRemaining = startingNumberOfScans;
        extractsRemaining = startingNumberOfExtracts;

        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CreateGrid()
    {
        RandomizeGridResources();

        for (int col = 0; col < numOfColumns; col++)
        {
            for (int row = 0; row < numOfRows; row++)
            {
                GridGenerator.GetInstance().miningTileArray[col,row] = CreateMiningTile(col, row);
                GridGenerator.GetInstance().miningTileArray[col, row].GetComponent<TileManager>().gridManager = this;
            }
        }
    }
    private GameObject CreateMiningTile(int columnPos, int rowPos)
    {
        GameObject instatiatedObject = Instantiate(miningUnitPrefab, new Vector3(columnPos * gridSpacing, -rowPos * gridSpacing, 0), Quaternion.Euler(new Vector3(270, 0, 0)));
        instatiatedObject.GetComponent<MiningUnitAttributes>().columnPosition = columnPos;
        instatiatedObject.GetComponent<MiningUnitAttributes>().rowPosition = rowPos;
        instatiatedObject.transform.SetParent(gameObject.transform);
        UpdateTileBasedOnType(instatiatedObject, columnPos, rowPos);

        return instatiatedObject;
    }

    private void ResetGrid()
    {
        Array.Clear(tileTypeArray, 0, tileTypeArray.Length);

        for (int col = 0; col < numOfColumns; col++)
        {
            for (int row = 0; row < numOfRows; row++)
            {
                GridGenerator.GetInstance().miningTileArray[numOfColumns, numOfRows].GetComponent<MiningUnitAttributes>().SetTileAttributes
                    (MiningUnitType.MINIMAL_RESOURCE, minimalValueMaterial,hoveredMinimalValueMaterial, minimalValue);
            }
        }
    }

    private void RandomizeGridResources()
    {
        int maxValueTilesPlacedCounter = 0;

        int minColumIndex =  scanExtent;
        int minRowIndex =   scanExtent;
        int maxColumIndex = numOfColumns - scanExtent;
        int maxRowIndex = numOfColumns - scanExtent;


        while (maxValueTilesPlacedCounter < numberOfMaxValueTiles)
        {
            int columnValue = UnityEngine.Random.Range(minColumIndex, maxColumIndex);
            int rowValue = UnityEngine.Random.Range(minColumIndex, maxColumIndex);

            if (CheckIfTileIsAvailable(columnValue,rowValue))
            {
                maxValueTilesPlacedCounter++;
                PopulateTilesWithValues(columnValue, rowValue);
            }
        }
    }

    private bool CheckIfTileIsAvailable(int colPosition, int rowPosition)
    {
        bool topLeftTileAvailable = tileTypeArray[colPosition - scanExtent, rowPosition - scanExtent] == MiningUnitType.MINIMAL_RESOURCE;
        bool topRightTileAvailable = tileTypeArray[colPosition + scanExtent, rowPosition - scanExtent] == MiningUnitType.MINIMAL_RESOURCE;
        bool bottomLeftTileAvailable = tileTypeArray[colPosition - scanExtent, rowPosition + scanExtent] == MiningUnitType.MINIMAL_RESOURCE;
        bool bottomRightTileAvailable = tileTypeArray[colPosition + scanExtent, rowPosition + scanExtent] == MiningUnitType.MINIMAL_RESOURCE;


        return topLeftTileAvailable && topRightTileAvailable && bottomLeftTileAvailable && bottomRightTileAvailable;
    }

    private void PopulateTilesWithValues(int columnPos, int rowPos)
    {
        for (int col = -scanExtent; col <=  scanExtent; col++)
        {
            for (int row = -scanExtent; row <= scanExtent; row++)
            {
                if (Mathf.Abs(col) == 2 || Mathf.Abs(row) == 2)
                {
                    tileTypeArray[columnPos + col, rowPos + row] = MiningUnitType.QUARTER_RESOURCE;
                }
                else
                {
                    tileTypeArray[columnPos + col, rowPos + row] = MiningUnitType.HALF_RESOURCE;
                }
            }

        }
        tileTypeArray[columnPos, rowPos] = MiningUnitType.MAX_RESOURCE;
    }

    void UpdateTileBasedOnType(GameObject tile, int columPos, int rowPos)
    {
        switch (tileTypeArray[columPos, rowPos])
        {
            case MiningUnitType.MAX_RESOURCE:
                tile.GetComponent<MiningUnitAttributes>().SetTileAttributes(MiningUnitType.MAX_RESOURCE, maxValueMaterial,hoveredMaxValueMaterial, maxValue);
                break;
            case MiningUnitType.HALF_RESOURCE:
                tile.GetComponent<MiningUnitAttributes>().SetTileAttributes(MiningUnitType.HALF_RESOURCE, halfValueMaterial, hoveredHalfValueMaterial, halfValue);
                break;
            case MiningUnitType.QUARTER_RESOURCE:
                tile.GetComponent<MiningUnitAttributes>().SetTileAttributes(MiningUnitType.QUARTER_RESOURCE, quarterValueMaterial, hoveredQuarterValueMaterial, quarterValue);
                break;
            case MiningUnitType.MINIMAL_RESOURCE:
                tile.GetComponent<MiningUnitAttributes>().SetTileAttributes(MiningUnitType.MINIMAL_RESOURCE, minimalValueMaterial, hoveredMinimalValueMaterial, minimalValue);
                break;
        }
    }

    public void UpdateTilesFromScan(int columPos, int rowPos)
    {
        for (int col = -scanExtent; col <= scanExtent; col ++)
        {   
            for (int row = -scanExtent; row <= scanExtent; row++)
            {
                if ((rowPos + row >= 0 && rowPos + row < GridGenerator.numberOfRows) && (columPos + col >= 0 && columPos + col < GridGenerator.numberOfColumns))
                {
                    GameObject tile = GridGenerator.GetInstance().miningTileArray[columPos + col, rowPos + row];
                    tile.GetComponent<MiningUnitAttributes>().SetMaterial(tile.GetComponent<MiningUnitAttributes>().scannedMaterial);
                    tile.GetComponent<MiningUnitAttributes>().tileHasBeenScanned = true;
                }
            }
        }
    }

    public void UpdateTilesFromExtract(int columnPos, int rowPos)
    {
        for (int col = -scanExtent; col <= scanExtent; col++)
        {
            for (int row = -scanExtent; row <= scanExtent; row++)
            {
                if ((rowPos + row >= 0 && rowPos + row < GridGenerator.numberOfRows) && (columnPos + col >= 0 && columnPos + col < GridGenerator.numberOfColumns))
                {
                    GameObject tile = GridGenerator.GetInstance().miningTileArray[columnPos + col, rowPos + row];

                    //Tile that was extracted
                    if (row == 0 && col == 0)
                    {
                        ReduceExtractedTile(tile);
                    }
                    else
                    {
                        ReduceSurroundingExtractedTiles(tile);
                    }
                }
            }
        }
    }

    public void ReduceExtractedTile(GameObject tile)
    {
        int pointsEarned = tile.GetComponent<MiningUnitAttributes>().currentTileValue;
        playerScore += pointsEarned;
        recentPointsEarned = pointsEarned;
        tile.GetComponent<MiningUnitAttributes>().SetTileAttributes(MiningUnitType.MINIMAL_RESOURCE, minimalValueMaterial, hoveredMinimalValueMaterial, minimalValue);
        tile.GetComponent<MiningUnitAttributes>().SetMaterial(minimalValueMaterial);
        tile.GetComponent<MiningUnitAttributes>().tileHasBeenScanned = true;
    }

    public void ReduceSurroundingExtractedTiles(GameObject tile)
    {
        switch (tile.GetComponent<MiningUnitAttributes>().unitType)
        {
            case MiningUnitType.MAX_RESOURCE:
                tile.GetComponent<MiningUnitAttributes>().SetTileAttributes(MiningUnitType.HALF_RESOURCE, halfValueMaterial, hoveredHalfValueMaterial, halfValue);
                tile.GetComponent<MiningUnitAttributes>().SetMaterial(tile.GetComponent<MiningUnitAttributes>().scannedMaterial);
                tile.GetComponent<MiningUnitAttributes>().tileHasBeenScanned = true;
                break;
            case MiningUnitType.HALF_RESOURCE:
                tile.GetComponent<MiningUnitAttributes>().SetTileAttributes(MiningUnitType.QUARTER_RESOURCE, quarterValueMaterial, hoveredQuarterValueMaterial, quarterValue);
                tile.GetComponent<MiningUnitAttributes>().SetMaterial(tile.GetComponent<MiningUnitAttributes>().scannedMaterial);
                tile.GetComponent<MiningUnitAttributes>().tileHasBeenScanned = true;
                break;
            case MiningUnitType.QUARTER_RESOURCE:
                tile.GetComponent<MiningUnitAttributes>().SetTileAttributes(MiningUnitType.MINIMAL_RESOURCE, minimalValueMaterial, hoveredMinimalValueMaterial, minimalValue);
                tile.GetComponent<MiningUnitAttributes>().SetMaterial(tile.GetComponent<MiningUnitAttributes>().scannedMaterial);
                tile.GetComponent<MiningUnitAttributes>().tileHasBeenScanned = true;
                break;
            case MiningUnitType.MINIMAL_RESOURCE:
                tile.GetComponent<MiningUnitAttributes>().SetMaterial(tile.GetComponent<MiningUnitAttributes>().scannedMaterial);
                tile.GetComponent<MiningUnitAttributes>().tileHasBeenScanned = true;
                break;
        }
    }

}
