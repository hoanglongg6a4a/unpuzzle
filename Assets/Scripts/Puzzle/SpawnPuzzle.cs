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
        // Kích th??c c?a m?i ô
        float cellSize = 1f;

        // V? trí g?c c?a b?ng (góc d??i cùng bên trái)
        Vector3 origin = new Vector3(-columns / 2f, -rows / 2f, 0f);

        // Tính toán v? trí c?a ô d?a trên hàng và c?t
        Vector3 cellPosition = origin + new Vector3(column, row, 0f) * cellSize;

        return cellPosition;
    }
    public void SpawnPuzzles()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                // L?y v? trí c?a ô trong b?ng
                Vector3 cellPosition = GetCellPosition(row, column);

                // L?y prefab puzzle t? danh sách theo ch? s? ng?u nhiên
                Puzzle randomPrefab = spawnPuzzleList[Random.Range(0, spawnPuzzleList.Count-1)];

                // Instantiate prefab puzzle t?i v? trí c?a ô
                Puzzle puzzle = Instantiate(randomPrefab, cellPosition, Quaternion.identity);
                // ??t puzzle vào ma tr?n 2 chi?u
                grid[row, column] = puzzle;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
