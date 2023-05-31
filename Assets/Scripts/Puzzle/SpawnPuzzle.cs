using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPuzzle : MonoBehaviour
{
    [SerializeField]
    private List<Puzzle> spawnPuzzleList;
    private Puzzle[,] grid;
    private int rows; 
    private int columns;
    private bool[,] blocked;
    private List<Puzzle> puzzlePrefabs = new List<Puzzle>();
    // Start is called before the first frame update
    void Start()
    {
        grid = new Puzzle[rows, columns]; 
        blocked = new bool[rows, columns];
        AddPrefab(spawnPuzzleList[0], 5);
        AddPrefab(spawnPuzzleList[1], 5);
        AddPrefab(spawnPuzzleList[2], 5);
        AddPrefab(spawnPuzzleList[3], 5);
        SpawnPuzzles();
    }
    public void init(int rows , int columns)
    {
        this.rows = rows;
        this.columns = columns;
    }
    public void AddPrefab(Puzzle prefab, int instanceCount)
    {
        for (int i = 0; i < instanceCount-1; i++)
        {
            Puzzle instance = Instantiate(prefab);
            instance.gameObject.SetActive(false);
            puzzlePrefabs.Add(instance);
        }
    }
  /*  public Puzzle GetRandomPrefab()
    {
        Puzzle randomPrefab = puzzlePrefabs[Random.Range(0, puzzlePrefabs.Count)];
        return randomPrefab;
    }*/
    public Puzzle GetInstanceFromPool()
    { 
        foreach (Puzzle instance in puzzlePrefabs)
        {

            if (!instance.gameObject.activeInHierarchy)
            {
                instance.gameObject.SetActive(true);

                return instance;
            }
        }
        return null; 
    }
    public Vector3 GetCellPosition(int row, int column)
    {
        float cellSize = 1f;
        Vector3 origin = new Vector3(-columns / 3f, -rows / 3f, 0f);
        Vector3 cellPosition = origin + new Vector3(column, row, 0f) * cellSize;
        return cellPosition;
    }
    public void SpawnPuzzles()
    {    
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                Puzzle puzzle = GetInstanceFromPool();
                if (puzzle != null)
                {           
                    grid[row, column] = puzzle;
                    puzzle.SetRowColumn(row, column);
                    puzzle.transform.position = GetCellPosition(row, column);                 
                    puzzle.gameObject.SetActive(true);
                }
            }
        }
    }
    public void DeletePos(int row, int column)
    {
        grid[row, column] = null;
    }
    public void SetPuzzlePosition(Puzzle puzzle, int newRow, int newColumn)
    {
        grid[puzzle.GetRow(), puzzle.GetColumn()] = null;
        puzzle.SetRowColumn(newRow, newColumn);
        grid[newRow, newColumn] = puzzle;
    }
    public int IsPositionEmpty(int row, int column)
    {
        int numRows = grid.GetLength(0);
        int numColumns = grid.GetLength(1);
        Debug.Log(row + "," + column);
        Debug.Log(numRows+","+numColumns);

        if (row < 0 || row >= numRows || column < 0 || column >= numColumns)
        {
            Debug.Log("Bay ra khỏi map");
            return 0;
        }

        if (grid[row, column] == null)
        {
            Debug.Log("Tiến tới");
            return 1;
        }
        else
        {
            Debug.Log("Bị kẹt");
            return 2;
        }
    }
    // Update is called once per frame
    void Update()
    {       
    }
}
