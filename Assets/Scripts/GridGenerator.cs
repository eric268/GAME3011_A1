using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public Vector3 startingPosition = Vector3.zero;
    public int numOfRows;
    public int numOfColumns;
    public GameObject miningUnitPrefab;
    public GameObject gridParentObject;
    public float gridSpacing;
    GameObject[,] miningUnitArray;
    // Start is called before the first frame update
    void Start()
    {
        miningUnitArray = new GameObject[numOfRows,numOfColumns];
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void CreateGrid()
    {
        for (int col = 0; col < numOfColumns; col++)
        {
            for (int row = 0; row < numOfRows; row++)
            {

                miningUnitArray[col,row] = CreateMiningTile(col, row);

            }
        }
    }
    private GameObject CreateMiningTile(int columnPos, int rowPos)
    {
        GameObject instatiatedObject = Instantiate(miningUnitPrefab, new Vector3(columnPos * gridSpacing, -rowPos * gridSpacing, 0), Quaternion.Euler(new Vector3(270, 0, 0)));

        instatiatedObject.GetComponent<MiningUnitAttributes>().columnPosition = columnPos;
        instatiatedObject.GetComponent<MiningUnitAttributes>().rowPosition = rowPos;

        return instatiatedObject;
    }

    private void RandomizeGridResources(int [,] resourceArray)
    {

    }
}
