using UnityEngine;

public class GameConfig : MonoBehaviour
{
    #region GAME PLAY
    [Space(8.0f)]
    [Header("Game Play")]
    #endregion

    #region Puzzle
    [Header("PUZZLW STATUS")]
    [Tooltip("Speed")]
    [SerializeField]
    private float speed = 5f;
    [Tooltip("Row Num")]
    [SerializeField]
    private int row = 5;
    [Tooltip("Col Num")]
    [SerializeField]
    private int col = 5;
    [Tooltip("Cell Size")]
    [SerializeField]
    private float cellsize = 0.9f;
    #endregion
    #region Coin
    [Header("POOLCOIN")]
    [Tooltip("PoolSize")]
    [SerializeField]
    private int poolSize = 10;
    #endregion
    public float Speed { get => speed; set => speed = value; }
    public int Row { get => row; set => row = value; }
    public int Col { get => col; set => col = value; }
    public float Cellsize { get => cellsize; set => cellsize = value; }
    public int PoolSize { get => poolSize; set => poolSize = value; }
  


}
