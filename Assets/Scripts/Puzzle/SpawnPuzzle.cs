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
    private 
    // Start is called before the first frame update
    void Start()
    {
        rows = 5;
        columns = 5;
        grid = new Puzzle[rows, columns]; // kh?i t?o m?ng puzzle
        blocked = new bool[rows, columns];
    }
    public Vector3 GetCellPosition(int row, int column)
    {
        // K�ch th??c c?a m?i �
        float cellSize = 1f;

        // V? tr� g?c c?a b?ng (g�c d??i c�ng b�n tr�i)
        Vector3 origin = new Vector3(-columns / 2f, -rows / 2f, 0f);

        // T�nh to�n v? tr� c?a � d?a tr�n h�ng v� c?t
        Vector3 cellPosition = origin + new Vector3(column, row, 0f) * cellSize;

        return cellPosition;
    }
    public void SpawnPuzzles()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                // L?y v? tr� c?a � trong b?ng
                Vector3 cellPosition = GetCellPosition(row, column);

                // L?y prefab puzzle t? danh s�ch theo ch? s? ng?u nhi�n
                Puzzle randomPrefab = spawnPuzzleList[Random.Range(0, spawnPuzzleList.Count-1)];

                // Instantiate prefab puzzle t?i v? tr� c?a �
                Puzzle puzzle = Instantiate(randomPrefab, cellPosition, Quaternion.identity);
                // ??t puzzle v�o ma tr?n 2 chi?u
                grid[row, column] = puzzle;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
