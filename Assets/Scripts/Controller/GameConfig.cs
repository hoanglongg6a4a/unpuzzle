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
 /*   [Tooltip("Nunber of Puzzel")]
    public int PuzzelNum = 2;*/

    public float Speed { get => speed; set => speed = value; }
    #endregion


}
