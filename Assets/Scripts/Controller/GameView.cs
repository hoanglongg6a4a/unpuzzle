using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MoveCount;
    [SerializeField] private GameObject EndPanel;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject Config;
    [SerializeField] private TextMeshProUGUI LevelNum;
    [SerializeField] private TextMeshProUGUI coinText;
    private const string Level = "Level";
    private void Awake()
    {
        int levelGame = PlayerPrefs.GetInt(Level);
        LevelNum.text = levelGame.ToString();
    }
    public void SetMoveCount(int count)
    {
        MoveCount.text = count.ToString();
    }    
    public void ShowEndPanel()
    {
        EndPanel.SetActive(true);
    }
    public void ShowConfig()
    {
        Config.SetActive(true);
    }  public void ExitConfig()
    {
        Config.SetActive(false);
    }
    public void ShowWinPanel()
    {
        WinPanel.SetActive(true);
    } 
}
