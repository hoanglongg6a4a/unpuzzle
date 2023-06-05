using System.Collections;
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
    [SerializeField] CoinPool coinPool;
    [SerializeField] GameObject targetCoin;
    private const string Level = "Level";
    private int coinReward;
    private int levelGame;
    private int countMove;
    private string tileUseBomb = "Tap a tile to place a Bomb";
    private string tileUseHammer = "Tap a tile to place a Destroy";
    private void Awake()
    {
        Application.targetFrameRate= 60;
        IsGameStartedForTheFirstTime();
        //PlayerPrefs.SetInt(Level, 0);
        levelGame = PlayerPrefs.GetInt(Level);
        // Only have 6 level :V
        if(levelGame > 6  )
        {
            PlayerPrefs.SetInt(Level, 0);
            levelGame = PlayerPrefs.GetInt(Level);
        }
        if (levels[levelGame] != null)
        {
            countMove = levels[levelGame].getMoveCount();
            coinReward = levels[levelGame].getCoinReward();
            view.SetMoveCount(countMove);
            spawnPuzzle.InitTable(model.Row, model.Col, model.Cellsize, levels[levelGame].getList(), countMove, view.SetMoveCount, view.SetTopPanel);
            coinPool.Init(model.PoolSize);
        }
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
        spawnPuzzle.SetUseHammer(true);
        view.SetTopPanel(false,true,tileUseHammer);
    }
    public void UseBomb()
    {
        spawnPuzzle.SetUseBomb(true);
        view.SetTopPanel(false,true,tileUseBomb);
    }
    public void GetReward()
    {
        StartCoroutine(ShowCoinsSequentially());
    }
    private IEnumerator ShowCoinsSequentially()
    {
        for (int i = 0; i < coinReward; i++)
        {
            CoinReward coin = coinPool.GetPooledCoin();
            coin.ShowReward(targetCoin.transform);
            view.SetCoin();
            yield return new WaitForSeconds(0.1f); 
        }
        SceneManager.LoadScene(model.SenceGamePlayName);
    }
    public void Restart()
    {
        SceneManager.LoadScene(model.SenceGamePlayName);
    }    
    private void Update()
    {   
        if (spawnPuzzle.CheckAllObjectsHidden(spawnPuzzle.GetNodeDict()) && !spawnPuzzle.GetEndGame())
        {
            view.ShowWinPanel();
            levelGame++;
            PlayerPrefs.SetInt(Level, levelGame);
            spawnPuzzle.SetEndGame(true);
        }
        if(spawnPuzzle.GetCountMove() == 0)
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
