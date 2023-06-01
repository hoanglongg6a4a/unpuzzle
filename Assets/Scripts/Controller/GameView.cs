using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private Text MoveCount;
    public void SetMoveCount(int count)
    {
        MoveCount.text = count.ToString();
    }    
}
