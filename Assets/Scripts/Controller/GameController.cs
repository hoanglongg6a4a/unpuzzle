using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum NodeType
{
    None , Left , Right , Up , Down , Rotate , Saw
}
public class GameController : MonoBehaviour
{
    [Header("MVC")]
    [SerializeField] GameConfig model;
    [SerializeField] GameView view;
    [SerializeField] GameAudio audio;
    [Header("Preference")]
    [SerializeField] SpawnPuzzle spawnPuzzle;
    [SerializeField] private List<Level> levels;
    private const string Level = "Level";
    private int levelGame;
    private int CountMove;
    private void Awake()
    {
        CountMove = 20;
        view.SetMoveCount(CountMove);
        PlayerPrefs.SetInt(Level, 0);
        //IsGameStartedForTheFirstTime();
        levelGame = PlayerPrefs.GetInt(Level);
        Debug.Log(levelGame);
        spawnPuzzle.initTable(model.Row, model.Col, model.Cellsize, levels[levelGame].getList(),CountMove,view.SetMoveCount);
    }
    private void IsGameStartedForTheFirstTime()
    {
        if (!PlayerPrefs.HasKey("IsGameStartedForTheFirstTime"))
        {
            PlayerPrefs.SetInt(Level, 0);
            PlayerPrefs.SetInt("IsGameStartedForTheFirstTime", 0);
        }
    }
    public void UseHammer()
    {
        spawnPuzzle.UseHammer = true;
    }
    public void UseBomb()
    {
        spawnPuzzle.UseBomb = true;
    }
    public void Restart()
    {
        SceneManager.LoadScene("GamePlay");
    }    
    private void Update()
    {
        if (spawnPuzzle.CheckAllObjectsHidden(spawnPuzzle.GetNodeDict()) && !spawnPuzzle.getEndGame())
        {
            view.ShowWinPanel();
            levelGame++;
            PlayerPrefs.SetInt("Level", levelGame);
            spawnPuzzle.SetEndGame(true);
        }
        if(spawnPuzzle.getCountMove() == 0)
        {
            spawnPuzzle.SetEndGame(true);
            view.ShowEndPanel();
        }    
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D collider2D = Physics2D.OverlapPoint(touchPos);
            spawnPuzzle. TouchAction(collider2D);
        }
    }
}
