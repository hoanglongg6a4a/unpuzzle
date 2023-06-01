using UnityEngine;

public class GameConfig : MonoBehaviour
{
    #region GAME PLAY
    [Space(8.0f)]
    [Header("Game Play")]
    #endregion

    #region Puzzle
    [Header("PUZZLW STATUS")]
    [Tooltip("Speed Speed")]
    private float speed = 5f;
    private int row = 5;
    private int col = 5;
    private float cellsize = 0.9f;
 /*   [Tooltip("Nunber of Puzzel")]
    public int PuzzelNum = 2;*/

    public float Speed { get => speed; set => speed = value; }
    public int Row { get => row; set => row = value; }
    public int Col { get => col; set => col = value; }
    public float Cellsize { get => cellsize; set => cellsize = value; }
    #endregion


}
