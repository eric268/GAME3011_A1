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
    GameObject[,] miningUnitArray;
    MiningUnitType[,] tileTypeArray;

    private int numberOfMaxValueTiles;

    [Header("Materials")]
    public Material startingMaterial;
    public Material maxValueMaterial;
    public Material halfValueMaterial;
    public Material quarterValueMaterial;
    public Material minimalValueMaterial;

    [Header("Tile Values")]
    public int maxValue;
    public int halfValue;
    public int quarterValue;
    public int minimalValue;

    public int scanExtent;


    // Start is called before the first frame update
    void Start()
    {
        numberOfMaxValueTiles = 15;
        miningUnitArray = new GameObject[numOfRows,numOfColumns];
        tileTypeArray = new MiningUnitType[numOfRows, numOfColumns];

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
                miningUnitArray[col,row] = CreateMiningTile(col, row);
                miningUnitArray[col, row].GetComponent<MeshRenderer>().material = miningUnitArray[col, row].GetComponent<MiningUnitAttributes>().currentMaterial;
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
                miningUnitArray[numOfColumns, numOfRows].GetComponent<MiningUnitAttributes>().SetTileAttributes(MiningUnitType.MINIMAL_RESOURCE, minimalValueMaterial, minimalValue);
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
                tile.GetComponent<MiningUnitAttributes>().SetTileAttributes(MiningUnitType.MAX_RESOURCE, maxValueMaterial, maxValue);
                break;
            case MiningUnitType.HALF_RESOURCE:
                tile.GetComponent<MiningUnitAttributes>().SetTileAttributes(MiningUnitType.HALF_RESOURCE, halfValueMaterial, halfValue);
                break;
            case MiningUnitType.QUARTER_RESOURCE:
                tile.GetComponent<MiningUnitAttributes>().SetTileAttributes(MiningUnitType.QUARTER_RESOURCE, quarterValueMaterial, quarterValue);
                break;
            case MiningUnitType.MINIMAL_RESOURCE:
                tile.GetComponent<MiningUnitAttributes>().SetTileAttributes(MiningUnitType.MINIMAL_RESOURCE, minimalValueMaterial, minimalValue);
                break;
        }
    }
}
