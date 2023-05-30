using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPuzzle : MonoBehaviour
{
    [SerializeField]
    private List<Puzzle> spawnPuzzleList;
    private int rows; 
    private int columns;
    private bool[,] blocked;
    private Puzzle[,] grid;
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
        for (int i = 0; i < spawnPuzzleList.Count; i++)
        {
            Puzzle instance = Instantiate(prefab);
            instance.gameObject.SetActive(false);
            puzzlePrefabs.Add(instance);
        }
    }

    public Puzzle GetRandomPrefab()
    {
        Puzzle randomPrefab = puzzlePrefabs[Random.Range(0, puzzlePrefabs.Count)];
        return randomPrefab;
    }

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
        float cellSize = 0.8f;
        Vector3 origin = new Vector3(-columns / 2f, -rows / 2f, 0f);
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
                    puzzle.transform.position = GetCellPosition(row, column);                 
                    puzzle.gameObject.SetActive(true);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
